using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConchEffect : MonoBehaviour
{
    protected Animator _animator;
    protected string _eventName = "ShowConchEffect";

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        EventMgr.GetInstance().AddLinstener<string>(_eventName, ShowConchEffect);
    }



    /// <summary>
    /// 接受 conch 脚本的事件触发
    /// </summary>
    /// <param name="info"></param>
    public void ShowConchEffect(string info)
    {
        _animator.SetBool("show",true);
    }

    /// <summary>
    /// 动画事件 重置参数
    /// </summary>
    public void ResetAnimatorParameter()
    {
        _animator.SetBool("show",false);
    }
}
