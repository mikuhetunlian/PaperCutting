using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadder : PlayerAblity
{

    ///�����ӵ��ٶ�
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
    /// �������� ��ladder����Ϊ�ĵط�
    /// </summary>
    protected void HandleLadderClimbing()
    {
        if (!AbilityPermitted)
        {
            return;
        }


        if(LadderCollider)
        {
            //����������� �� �պ���������Ļ�
            if (_movement.CurrentState == PlayerStates.MovementStates.LadderClimbing
                && _playerController.State.IsGrounded
                && !_playerController.IsGravityActive)
            {
                GetOffTheLadder();
            }

            //������������ӵ�״̬
            if (_movement.CurrentState != PlayerStates.MovementStates.LadderClimbing)
            {
                if (_verticalInput > 0)
                {
                    StartClimbing();
                }
            }
            //����������ӵ�״̬
            if (_movement.CurrentState == PlayerStates.MovementStates.LadderClimbing)
            {
                Climbing();
            }



        }
       
    }


    protected void StartClimbing()
    {
        _movement.ChangeState(PlayerStates.MovementStates.LadderClimbing);
        //�ر�����
        _playerController.GravityActive(false);
    }

    protected void Climbing()
    {
        _playerController.SetVerticalForce(_verticalInput * LadderClimbingSpeed);
    }



    /// <summary>
    /// �뿪ladderҪ������ 
    /// ����������ability����
    /// </summary>
    public void GetOffTheLadder()
    {
        _movement.ChangeState(PlayerStates.MovementStates.Idle);
        _playerController.GravityActive(true);
        Debug.Log("����������");
    }



















    /// <summary>
    /// ����������� ladder �� collider2D
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
    /// �Ƴ��뿪ʱ�� ladder �� collider2D
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
