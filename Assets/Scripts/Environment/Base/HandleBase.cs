using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handle 的基类
/// </summary>
public class HandleBase : MonoBehaviour
{
    public enum HandleType
    {
        ///Handle 只能操纵一次
        OnceOnly,
        ///Handle 能反复操纵
        Repeating
    }

    public enum HandleState
    {
        ///Handle 生效
        On,
        ///Handle 不生效
        Off
    }

    public HandleType handleType = HandleType.Repeating;
    public HandleState handleState = HandleState.Off;
    public bool CanBeControl => _canBeControl;



    protected Animator _animator;
    protected bool _canBeControl = false;
    protected bool _onlyOnceCanBeControl = true;
    protected bool _isChanging;


    private void Start()
    {
        Initilization();
    }


    protected virtual void Initilization()
    {
        _animator = GetComponent<Animator>();
    }



    /// <summary>
    /// 打开Handle后要做的事情 , spine动画事件
    /// </summary>
    protected virtual void TurnOn()
    { 

    }

    /// <summary>
    /// 关闭Handle后要做的事情, spine动画事件
    /// </summary>
    protected virtual void TurnOff()
    {
        
    }

    /// <summary>
    /// Hanlde旋转结束 ,spine动画事件，base()有内容
    /// </summary>
    protected virtual void ChangeOver()
    {
        _isChanging = false;
    }


    protected virtual void Update()
    {

        if (_canBeControl &&
            InputManager.GetInstance().ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonDown)
        {

            switch(handleType)
            {
                case HandleType.OnceOnly:
                    //如果是OnceOnly类型的话，只能触发一次，可以通过  方法来重置操作
                    if (_onlyOnceCanBeControl)
                    {
                        ChangeHandleState();
                        _onlyOnceCanBeControl = false;
                    }
                    break;

                case HandleType.Repeating:
                    ChangeHandleState();
                    break;

            }
        }
    }



    /// <summary>
    /// 改变handle状态
    /// </summary>
    protected virtual void ChangeHandleState()
    {
        //如果还在转的过程，handle不能被操控
        if (_isChanging)
        {
            return;
        }

        if (handleState == HandleState.Off)
        {
            handleState = HandleState.On;
            TurnOnChangeAnimatorParameter();
        }
        else if (handleState == HandleState.On)
        {
            handleState = HandleState.Off;
            TurnOffChangeAnimatorParameter();
        }
        _isChanging = true;

    }

    /// <summary>
    /// TurnOn 时候要 改变的Animator参数
    /// </summary>
    protected virtual void TurnOnChangeAnimatorParameter()
    {
        
    }

    /// <summary>
    /// TurnOff 时候要 改变的Animator参数
    /// </summary>
    protected virtual void TurnOffChangeAnimatorParameter()
    {

    }



    /// <summary>
    /// 重置Handle的状态，包括 初始状态，，OnlyOnce类型下可以再次被操纵的开关
    /// </summary>
    public virtual void ResetHandleState()
    {
        if (handleType == HandleType.OnceOnly)
        {
            _onlyOnceCanBeControl = true;
        }

        handleState = HandleState.Off;
    }


    /// <summary>
    /// player 进入trigger，handle可以被操控
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            _canBeControl = true;
            DoWhenPlayerIn();
        }
    }

    /// <summary>
    /// player 进入trigger 后要做的其他的事情
    /// </summary>
    protected virtual void DoWhenPlayerIn()
    {
        
    }


    /// <summary>
    /// player 离开trigger，handle可以被操控
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void  OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            _canBeControl = false;
            DoWhenPlayerExit();
        }
    }

    /// <summary>
    /// player 进入trigger 后要做的其他的事情
    /// </summary>
    protected virtual void DoWhenPlayerExit()
    {

    }
}
