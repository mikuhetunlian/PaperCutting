using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerAccessories : MonoBehaviour
{

    public GameObject effect;
    /// 特效展开的时候要转移到的摄像机
    public CinemachineVirtualCamera GreatSizeCamera;
    ///移动到GreatSizeCamera需要的过度时间
    public float BlendTime;
    protected bool _canBeTouch;
    protected Jump _jump;

    private void Awake()
    {
        _canBeTouch = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && _canBeTouch)
        {
            Touch touch = collision.GetComponent<Touch>();
            touch.SetTouchIn();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && _canBeTouch)
        {
            if (InputManager.GetInstance().ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonDown)
            {
                DoWhenPlayerTouch(other);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && _canBeTouch)
        {
            Touch touch = collision.GetComponent<Touch>();
            touch.SetTouchOut();
        }
    }



    /// <summary>
    /// 当 player 触碰到自己时候 
    /// </summary>
    /// <param name="collision"></param>
    protected void DoWhenPlayerTouch(Collider2D collision)
    {
        //激活特效
        effect.SetActive(true);
        //添加功能，固定在玩家的右上角
        KeepAtPoint keep = this.gameObject.AddComponent<KeepAtPoint>();
        keep.Target = collision.gameObject;
        keep.SetOffset(new Vector2(2, 2));
        //重置player的touch Animator参数
        Touch touch = collision.GetComponent<Touch>();
        touch.SetTouchOut();
        //切换摄像机
        CameraMgr.GetInstance().ChangeCamera(GreatSizeCamera, BlendTime);
        //玩家跳跃高度增加
        _jump = collision.gameObject.GetComponent<Jump>();
        _jump.JumpHeight = 18;

        _canBeTouch = false;

        Invoke("EventTriggerRecover", BlendTime);
    }

    protected void EventTriggerRecover()
    {
        EventMgr.GetInstance().EventTrigger<string>("ChangeToGreateCamera", "ChangeToGreateCamera");
    }

    private void OnDestroy()
    {
        _jump.JumpHeight = 8;
    }
}
