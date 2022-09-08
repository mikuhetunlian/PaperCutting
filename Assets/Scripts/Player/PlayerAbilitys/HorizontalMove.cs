using UnityEngine;

public class HorizontalMove :PlayerAblity
{


    public float speed;
    public bool move;
    private Rigidbody2D rbody;


    public override void Initialization()
    {
        base.Initialization();
        speed = 16f;
        //Ìí¼Ó°´¼ü¼àÌý
        EventMgr.GetInstance().AddLinstener<KeyCode>("GetKey", GetKey);
        EventMgr.GetInstance().AddLinstener<KeyCode>("GetKeyUp", GetKeyUp);
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
        if (!abilityPermitted)
        {
            EventMgr.GetInstance().RemoveLinstener<KeyCode>("GetKey", GetKey);
            EventMgr.GetInstance().RemoveLinstener<KeyCode>("GetKeyUp", GetKeyUp);
        }
        else
        {
            EventMgr.GetInstance().AddLinstener<KeyCode>("GetKey", GetKey);
            EventMgr.GetInstance().AddLinstener<KeyCode>("GetKeyUp", GetKeyUp);
        }
    }


    public override void ProcessAbility()
    {
     
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
