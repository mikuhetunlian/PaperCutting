using UnityEngine;

public class HorizontalMove : PlayerAblity
{


    public Player.FacingDirections facingDir = Player.FacingDirections.Right;
    public float speed;
    public bool move;
    private Rigidbody2D rbody;
    [SerializeField]
    private Vector2 _newPostion;


    protected float _horizontalMovement;
    protected float _horizontalMovementForce;

    public override void Initialization()
    {
        base.Initialization();
        speed = 18f;
    }

    public override void GetComponents()
    {
        base.GetComponents();
        rbody = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    private void GetKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.A:
                if (AbilityPermitted)
                {
                    move = true;

                    if (_playerController.State.isCollidingBelow)
                    {
                        _movement.ChangeState(PlayerStates.MovementStates.Walking);
                    }

                    if (!_playerController.State.isCollidingLeft)
                    {
                        rbody.velocity = new Vector2(-speed, rbody.velocity.y);
                    }
                    else
                    {
                        rbody.velocity = new Vector2(0, rbody.velocity.y);
                        float hitPointX = 0;
                        foreach (RaycastHit2D info in _playerController.hitInfos[RaycastDirection.Left])
                        {
                            if (info.collider != null)
                            {
                                hitPointX = info.point.x;
                            }
                        }
                        transform.position = new Vector3(hitPointX + _playerController.Collider.bounds.extents.x, transform.position.y);
                    }
                }

                break;
            case KeyCode.D:
                if (AbilityPermitted)
                {
                    move = true;
                    if (_playerController.State.isCollidingBelow)
                    {
                        _movement.ChangeState(PlayerStates.MovementStates.Walking);
                    }
                    if (!_playerController.State.isCollidingRight)
                    {
                        rbody.velocity = new Vector2(speed, rbody.velocity.y);
                    }
                    else
                    {
                        rbody.velocity = new Vector2(0, rbody.velocity.y);
                        float hitPointX = 0;
                        foreach (RaycastHit2D info in _playerController.hitInfos[RaycastDirection.Right])
                        {
                            if (info.collider != null)
                            {
                                hitPointX = info.point.x;
                            }
                        }
                        transform.position = new Vector3(hitPointX - _playerController.Collider.bounds.extents.x, transform.position.y);
                    }
                }

                break;
        }
    }

    private void GetKeyUp(KeyCode key)
    {
        if (AbilityPermitted)
        {
            switch (key)
            {
                case KeyCode.A:
                    move = false;
                    if (_playerController.State.isCollidingBelow)
                    {
                        _movement.ChangeState(PlayerStates.MovementStates.Idle);
                    }
                    rbody.velocity = new Vector2(0, rbody.velocity.y);
                    break;
                case KeyCode.D:
                    move = false;
                    if (_playerController.State.isCollidingBelow)
                    {
                        _movement.ChangeState(PlayerStates.MovementStates.Idle);
                    }
                    rbody.velocity = new Vector2(0, rbody.velocity.y);
                    break;
            }
        }

    }


    public override void PermitAbility(bool abilityPermitted)
    {
        base.PermitAbility(abilityPermitted);
    }

    public override void HandleInput()
    {
        _horizontalMovement = _horizontalInput;

    }


    public override void ProcessAbility()
    {
        //SetFacingDir()
        HorizontalMovement();
    }


    protected void LeftPressed()
    {
        _playerController.SetHorizontalForce(-speed);
    }

    protected void LeftUp()
    {
        _playerController.SetHorizontalForce(0);
    }

    protected void RightPressed()
    {
        _playerController.SetHorizontalForce(speed);
    }

    protected void RightUp()
    {
        _playerController.SetHorizontalForce(0);
    }


    /// <summary>
    /// 检测当前的朝向
    /// </summary>
    protected void SetFacingDir()
    {
        if (_horizontalMovement > 0)
        {
            facingDir = Player.FacingDirections.Right;
        }
        if (_horizontalMovement < 0) 
        {
            facingDir = Player.FacingDirections.Left;
        }
    }

    /// <summary>
    /// 根据input输入来 改变 newPostion 的 x分量值
    /// </summary>
    public virtual void HorizontalMovement()
    {
        if (AbilityPermitted)
        {
            if (_playerController.State.isCollidingBelow 
                && _movement.CurrentState != PlayerStates.MovementStates.LadderClimbing //不在爬梯子
                && _movement.CurrentState != PlayerStates.MovementStates.Jumping)//不在跳跃
            {
                _movement.ChangeState(PlayerStates.MovementStates.Walking);
            }
            
            _playerController.SetHorizontalForce(_horizontalMovement * speed);
        }
       
    }

    /// <summary>
    /// 获得射线检测触碰到的点的x坐标
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    protected float GetHitPointX(RaycastDirection dir)
    {
        float hitPointX = 0;
        foreach (RaycastHit2D info in _playerController.hitInfos[dir])
        {
            if (info.collider != null)
            {
                hitPointX = info.point.x;
            }
        }
        return hitPointX;
    }

  

    protected override void InitializeAnimatorParameter()
    {
        RegisterAnimatorParameter("walk", AnimatorControllerParameterType.Bool);
        RegisterAnimatorParameter("idle", AnimatorControllerParameterType.Bool);
    }

    public override void UpdateAnimator()
    {
        AnimatorHelper.UpdateAnimatorBool(_animator, "walk", (_movement.CurrentState == PlayerStates.MovementStates.Walking), _player._animatorParameterList);
        AnimatorHelper.UpdateAnimatorBool(_animator, "idle", (_movement.CurrentState == PlayerStates.MovementStates.Idle), _player._animatorParameterList);
    }

}
