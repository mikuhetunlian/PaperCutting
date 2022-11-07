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

        //只在接触的一瞬间给竖直向上的速度 并切换面朝向
        if (_addVerticalSpeed)
        {
            _horizontalMove = playerController.GetComponent<HorizontalMove>();
            _horizontalMove.AbilityPermitted = false;
            playerController.SetHorizontalForce(0);
            InputManager.GetInstance().InputDetectionActive = false;
         
            //每次跳到bubble 上 激活 rainBubbleEffect
            _rainBubbleEffect.SetActive(true);
            MusicMgr.GetInstance().PlayMusicEffect(bubbleSong, false);
            ////把上一个 rainBubble的速度控制清除
            BreakOtherRainBubblesEffect();

            float gravity = Mathf.Abs(playerController.Parameters.Gravity);
            playerController.SetVerticalForce(Mathf.Sqrt(2 * gravity * jumpHeight));

            //根据水平速度的正负来改变相应的面朝向
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
        ////如果 玩家移动操控了 取消水平速度的保持
        //if (playerController._player.LinkedInputManager.PrimaryMovement.x != 0 && _addSpeed && _playerCanControl)
        //{
        //    playerController.SetHorizontalForce(0);
        //    _addSpeed = false;
        //}
        //如果 玩家落到了地面上 取消水平速度的保持并在这一瞬间设置为0
        if (playerController.State.IsGrounded)
        {
            Debug.Log("落地");
            playerController.SetHorizontalForce(0);
            _rainBubbleAddSpeed = false;
            CameraMgr.GetInstance().ResetCurrnteCameraOffset();
            ActiveHorizontalMove();
            return;
        }
        //如果在这 说明玩家既没有操控 也还没有落到地面  就继续保持水平的移动速度
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
