using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndPull : PlayerAblity
{

    protected bool _controlAble;
    /// <summary>
    /// �ж��ܲ����ƺ���
    /// </summary>
    public override void HandleInput()
    {
        if (_inputManager.ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonPressed)
        {
            _controlAble = true;

        }
        if (_inputManager.ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonUp)
        {
            _controlAble = false;
        }
    }

    public override void ProcessAbility()
    {
        PushOrPull();
    }

    /// <summary>
    /// ����Player.FacingDirections��_horizontalInput��ʵ���ƺ���
    /// �Ƶ�ʱ���뻮��̫Զ���Ƶ��ٶ�Ҫ���� ��д����4
    /// ����ʱ��Ҫ������player�������ٶ�Ҫ��player�ƶ����ٶ�һ�� ��д����16
    /// </summary>
    public void PushOrPull()
    {

        if (_controlAble && _playerController.State.isDetectControlableObject)
        {
   
            //���� ����
            if (_horizontalInput > 0 && _player.CurrentFaceingDir == Player.FacingDirections.Right)
            { 
                _playerController.ControlAbleObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * Mathf.Abs(4);
            }

            //���� ����
            if (_horizontalInput > 0 && _player.CurrentFaceingDir == Player.FacingDirections.Left)
            {
         
                _playerController.ControlAbleObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * Mathf.Abs(16);
            }

            //���� ����
            if (_horizontalInput < 0 && _player.CurrentFaceingDir == Player.FacingDirections.Left)
            {

                _playerController.ControlAbleObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 4;
            }

            //���� ����
            if (_horizontalInput < 0 && _player.CurrentFaceingDir == Player.FacingDirections.Right)
            {
          
                _playerController.ControlAbleObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 16;
            }
        }


    }

}
