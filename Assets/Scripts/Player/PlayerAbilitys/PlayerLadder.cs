using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadder : PlayerAblity
{

    ///爬梯子的速度
    public float LadderClimbingSpeed;
    public bool LadderCollider { get { return _colliders.Count > 0; } }

    protected List<Collider2D> _colliders;
  


    public override void Initialization()
    {
        base.Initialization();
        _colliders = new List<Collider2D>();


    }



    public override void ProcessAbility()
    {
        base.ProcessAbility();

        HandleLadderClimbing();

    }

    /// <summary>
    /// 真正处理 在ladder上行为的地方
    /// </summary>
    protected void HandleLadderClimbing()
    {
        if (!AbilityPermitted)
        {
            return;
        }


        if(LadderCollider)
        {
            //如果在爬梯子 且 刚好碰到地面的话
            if (_movement.CurrentState == PlayerStates.MovementStates.LadderClimbing
                && _playerController.State.IsGrounded
                && !_playerController.IsGravityActive)
            {
                GetOffTheLadder();
            }

            //如果不在爬梯子的状态
            if (_movement.CurrentState != PlayerStates.MovementStates.LadderClimbing)
            {
                if (_verticalInput > 0)
                {
                    StartClimbing();
                }
            }
            //如果在爬梯子的状态
            if (_movement.CurrentState == PlayerStates.MovementStates.LadderClimbing)
            {
                Climbing();
            }



        }
       
    }


    protected void StartClimbing()
    {
        _movement.ChangeState(PlayerStates.MovementStates.LadderClimbing);
        //关闭重力
        _playerController.GravityActive(false);
    }

    protected void Climbing()
    {
        _playerController.SetVerticalForce(_verticalInput * LadderClimbingSpeed);
    }



    /// <summary>
    /// 离开ladder要做的事 
    /// 可以由其他ability调用
    /// </summary>
    public void GetOffTheLadder()
    {
        _movement.ChangeState(PlayerStates.MovementStates.Idle);
        _playerController.GravityActive(true);
        Debug.Log("开启了重力");
    }



















    /// <summary>
    /// 添加新碰到的 ladder 的 collider2D
    /// </summary>
    /// <param name="collider"></param>
    public void AddColliderLadder(Collider2D collider)
    {
        if (_colliders == null)
        {
            _colliders = new List<Collider2D>();
        }
        _colliders.Add(collider);
        Debug.Log(_colliders.Count);
    }

    /// <summary>
    /// 移除离开时的 ladder 的 collider2D
    /// </summary>
    /// <param name="collider"></param>
    public void RemoveColliderLadder(Collider2D collider)
    {
        if (_colliders == null)
        {
            return;
        } 
        _colliders.Remove(collider);
        Debug.Log(_colliders.Count);
    }



}
