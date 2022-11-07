using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 把剪刀从 被树枝缠绕的状态 中解放出来的Handle 
/// </summary>
public class ReleaseScissorTwineHandle : HandleBase
{

    protected string HandleOnAnimatorParameter = "TurnOn";
    protected string HandleOffAnimatorParameter = "TurnOff";

    /// <summary>
    /// 把剪刀从 被树枝缠绕的状态 中解放出来
    /// </summary>
    protected override void TurnOn()
    {
        ///使得 剪刀能剪 + 准备缠绕树枝脱落 
        ///这条事件链是 ReadyReleaseScissorTwine -> BrokenBranch + ReleaseScissorTwine
        EventMgr.GetInstance().EventTrigger<string>("ReadyReleaseScissorTwine", "ReadyReleaseScissorTwine");
        Debug.Log("ReadyReleaseScissorTwineHandle");
    }

    protected override void TurnOnChangeAnimatorParameter()
    {
        _animator.SetBool(HandleOnAnimatorParameter, true);
        _animator.SetBool(HandleOffAnimatorParameter, false);
    }

    protected override void TurnOffChangeAnimatorParameter()
    {
        _animator.SetBool(HandleOffAnimatorParameter, true);
        _animator.SetBool(HandleOnAnimatorParameter, false);
    }

}
