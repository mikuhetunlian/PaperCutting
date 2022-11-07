using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRainBubble : RainBubble
{
    protected HorizontalMove _horizontalMove;
    protected bool _playerCanControl;
    protected override void RainBubbleAddSpeed()
    {
        PlayerController playerController = _other.gameObject.GetComponent<PlayerController>();

        //ֻ�ڽӴ���һ˲�����ֱ���ϵ��ٶ� ���л��泯��
        if (_addVerticalSpeed)
        {
            _horizontalMove = playerController.GetComponent<HorizontalMove>();
            _horizontalMove.AbilityPermitted = false;
            playerController.SetHorizontalForce(0);
            InputManager.GetInstance().InputDetectionActive = false;
         
            //ÿ������bubble �� ���� rainBubbleEffect
            _rainBubbleEffect.SetActive(true);
            MusicMgr.GetInstance().PlayMusicEffect(bubbleSong, false);
            ////����һ�� rainBubble���ٶȿ������
            BreakOtherRainBubblesEffect();

            float gravity = Mathf.Abs(playerController.Parameters.Gravity);
            playerController.SetVerticalForce(Mathf.Sqrt(2 * gravity * jumpHeight));

            //����ˮƽ�ٶȵ��������ı���Ӧ���泯��
            if (horizontalSpeed > 0)
            {
                playerController._player.SetFace(Player.FacingDirections.Right);
            }
            if (horizontalSpeed < 0)
            {
                playerController._player.SetFace(Player.FacingDirections.Left);
            }

            _addVerticalSpeed = false;
        }
        ////��� ����ƶ��ٿ��� ȡ��ˮƽ�ٶȵı���
        //if (playerController._player.LinkedInputManager.PrimaryMovement.x != 0 && _addSpeed && _playerCanControl)
        //{
        //    playerController.SetHorizontalForce(0);
        //    _addSpeed = false;
        //}
        //��� ����䵽�˵����� ȡ��ˮƽ�ٶȵı��ֲ�����һ˲������Ϊ0
        if (playerController.State.IsGrounded)
        {
            Debug.Log("���");
            playerController.SetHorizontalForce(0);
            _rainBubbleAddSpeed = false;
            CameraMgr.GetInstance().ResetCurrnteCameraOffset();
            ActiveHorizontalMove();
            return;
        }
        //������� ˵����Ҽ�û�вٿ� Ҳ��û���䵽����  �ͼ�������ˮƽ���ƶ��ٶ�
        if (playerController._player.LinkedInputManager.PrimaryMovement.x == 0 && _addSpeed)
        { 
            playerController.SetHorizontalForce(horizontalSpeed);
        }
    }

    protected void ActiveHorizontalMove()
    {
        _horizontalMove.AbilityPermitted = true;
        InputManager.GetInstance().InputDetectionActive = true;
    }
}
