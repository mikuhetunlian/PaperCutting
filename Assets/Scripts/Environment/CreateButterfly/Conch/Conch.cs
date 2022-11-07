using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conch : MonoBehaviour
{

    public PaperButterfly paperButterfly;
    protected Animator _animator;
    protected bool _canBeTouch;

    void Start()
    {
        Initilization();
    }

    protected void Initilization()
    {
        _animator = GetComponent<Animator>();
        _canBeTouch = true;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && _canBeTouch)
        {
            Touch touch = collision.GetComponent<Touch>();
            touch.SetTouchIn();
            _canBeTouch = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Touch touch = collision.GetComponent<Touch>();
            touch.SetTouchOut();
            _canBeTouch = true;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (InputManager.GetInstance().ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonDown)
            {
                DoWhenPlayerTouch();
            }
        }
    }

    /// <summary>
    /// 吹海螺
    /// </summary>
    protected void DoWhenPlayerTouch()
    {
        _animator.SetBool("show", true);
        if (paperButterfly.CanActiveScissor)
        {
            //这里的 1.5f 指的是吹出号角后延迟多少秒激活剪刀
            EventMgr.GetInstance().EventTrigger<float>("ActiveScissor", 1.5f);
        }
    }

    /// <summary>
    /// 动画事件，触发海螺特效
    /// </summary>
    public void BlowConch()
    {
        EventMgr.GetInstance().EventTrigger<string>("ShowConchEffect", "ShowConchEffect");
    }

    /// <summary>
    /// 动画事件
    /// </summary>
    public void ResetAnimatorParameter()
    {
        _animator.SetBool("show", false);
    }
}
