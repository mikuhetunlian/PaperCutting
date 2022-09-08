using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerAblity
{
    private Rigidbody2D rbody;
    private Transform transform;

    //上升的速度
    public float upSpeed;
    //下降的速度
    public float fallSpeed;
    //上升时间倍增器
    public float upTimeMutilper;
    //下降时间倍增器
    public float fallTimeMutilper;
    public float fallAddSpeed;


    private PlayerController playerController;
    private PlayerControllerParameters parameter;
    private PlayerControllerState State;


    //是否在上升
    public bool isUping;
    //是否在下降
    public bool isFalling;
    //是否在跳跃 （包括上升和下降）
    public bool isJumping;
    //是否自然掉落
    public bool startNatureFall;
    public bool isTouchGround;
    //是否能再次跳跃
    public bool canJump;


    public override void Initialization()
    {
        base.Initialization();
        upSpeed = 15f;
        fallSpeed = 12f;
        fallTimeMutilper = 8f;
        upTimeMutilper = 5.5f;
        fallAddSpeed = 30;
        isFalling = true;
        EventMgr.GetInstance().AddLinstener<KeyCode>("GetKey", GetKey);
        EventMgr.GetInstance().AddLinstener<KeyCode>("GetKeyUp", GetKeyUp);
    }

    

    public override void GetComponents()
    {
        base.GetComponents();
        rbody = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        transform = GetComponent<Transform>();
        State = playerController.State;
        parameter = playerController.Parameters;
        
    }


    public override void ProcessAbility()
    {
        //自然掉落的处理
        if (!isJumping && !State.isCollidingBelow && !startNatureFall && AbilityPermitted)
        {
            startNatureFall = true;
            isFalling = true;
            StartCoroutine(Fall(Mathf.PI / 2));
        }


    }



    private void GetKey(KeyCode key)
    {
        if (key == KeyCode.Space && AbilityPermitted)
        {
            
            if ((State.isCollidingBelow && canJump) || _movement.CurrentState == PlayerStates.MovementStates.InBubble)
            {
                isUping = true;
                isFalling = false;
                isJumping = true;
                canJump = false;
                _movement.ChangeState(PlayerStates.MovementStates.Jumping);
                StartCoroutine(Up());
            }
        }
    }

    private void GetKeyUp(KeyCode key)
    {
      
        if (key == KeyCode.Space && AbilityPermitted)
        {
            if (isUping)
            {
                isUping = false;
                isFalling = true;
            }
            canJump = true;
        }
    }



    private IEnumerator Up()
    {
        float t = 0;
        while (isUping)
        {
            if (!AbilityPermitted)
            {
                yield break;
            }
            if (rbody.velocity.y < 0 || State.isCollidingAbove)
            {
                break;   
            }
            rbody.velocity = new Vector2(rbody.velocity.x, upSpeed * Mathf.Cos(t * upTimeMutilper));
            t +=  0.01f;
            yield return new WaitForFixedUpdate();
        }

        isUping = false;
        isFalling = true;
        if (rbody.velocity.y > 0)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, 0);
            StartCoroutine(Fall(Mathf.PI / (2 * upTimeMutilper)));
        }
        else
        {
            StartCoroutine(Fall(t));
        }
      
    }


    //下落函数
    private IEnumerator Fall(float fallStartTime)
    {
        float t = fallStartTime;
        float addSpeedTime = 0;
        while (isFalling)
        {
            if (!AbilityPermitted)
            {
                yield break;
            }
            if (playerController.fallHitInfo.collider != null)
            {
                
                if (Mathf.Abs(transform.position.y - playerController.fallHitInfo.point.y) <= 0.4f)
                {
                    break;
                }
            }
            if (t * fallTimeMutilper > Mathf.PI)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, -fallSpeed -  addSpeedTime * fallAddSpeed);
                addSpeedTime += Time.fixedDeltaTime;
            }
            else
            {
                rbody.velocity = new Vector2(rbody.velocity.x, fallSpeed * Mathf.Cos(t * fallTimeMutilper));
                t +=  Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        isFalling = false;
        isJumping = false;
        Invoke("ResetIsTouchGround", 0.15f);
        isTouchGround = true;
        startNatureFall = false;
        _movement.ChangeState(PlayerStates.MovementStates.Idle);

        float hitPointY = 0;
        if (playerController.fallHitInfo.collider != null)
        {
            hitPointY = playerController.fallHitInfo.point.y;
        }

        rbody.velocity = new Vector2(rbody.velocity.x, 0);
        transform.position = new Vector2(transform.position.x,hitPointY);
    }

    /// <summary>
    /// 重置IsTouchGround参数
    /// </summary>
    private void ResetIsTouchGround()
    {
        isTouchGround = false;
    }

    protected override void InitializeAnimatorParameter()
    {
        base.InitializeAnimatorParameter();
        RegisterAnimatorParameter("jump_up", AnimatorControllerParameterType.Bool);
        RegisterAnimatorParameter("jump_fall", AnimatorControllerParameterType.Bool);
        RegisterAnimatorParameter("touchGround", AnimatorControllerParameterType.Bool);
    }
    public override void UpdateAnimator()
    {
        base.UpdateAnimator();
        AnimatorHelper.UpdateAnimatorBool(_animator, "jump_up", isUping, _player._animatorParameterList);
        AnimatorHelper.UpdateAnimatorBool(_animator, "jump_fall", isFalling, _player._animatorParameterList);
        AnimatorHelper.UpdateAnimatorBool(_animator, "touchGround", isTouchGround, _player._animatorParameterList);
    }


}
