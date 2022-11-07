using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �Ѽ����� ����֦���Ƶ�״̬ �н�ų�����Handle 
/// </summary>
public class ReleaseScissorTwineHandle : HandleBase
{

    protected string HandleOnAnimatorParameter = "TurnOn";
    protected string HandleOffAnimatorParameter = "TurnOff";

    /// <summary>
    /// �Ѽ����� ����֦���Ƶ�״̬ �н�ų���
    /// </summary>
    protected override void TurnOn()
    {
        ///ʹ�� �����ܼ� + ׼��������֦���� 
        ///�����¼����� ReadyReleaseScissorTwine -> BrokenBranch + ReleaseScissorTwine
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
