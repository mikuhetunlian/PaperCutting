using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndPull : PlayerAblity
{

    protected bool _controlAble;
    /// <summary>
    /// 判断能不能推和拉
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
    /// 根据Player.FacingDirections和_horizontalInput来实现推和拉
    /// 推的时候不想划的太远，推的速度要降低 先写死了4
    /// 拉的时候要跟得上player，拉的速度要和player移动的速度一样 先写死了16
    /// </summary>
    public void PushOrPull()
    {

        if (_controlAble && _playerController.State.isDetectControlableObject)
        {
   
            //左推 向右
            if (_horizontalInput > 0 && _player.CurrentFaceingDir == Player.FacingDirections.Right)
            { 
                _playerController.ControlAbleObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * Mathf.Abs(4);
            }

            //右拉 向右
            if (_horizontalInput > 0 && _player.CurrentFaceingDir == Player.FacingDirections.Left)
            {
         
                _playerController.ControlAbleObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * Mathf.Abs(16);
            }

            //右推 向左
            if (_horizontalInput < 0 && _player.CurrentFaceingDir == Player.FacingDirections.Left)
            {

                _playerController.ControlAbleObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 4;
            }

            //左拉 向左
            if (_horizontalInput < 0 && _player.CurrentFaceingDir == Player.FacingDirections.Right)
            {
          
                _playerController.ControlAbleObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 16;
            }
        }


    }

}
