using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ScissorHandle : HandleBase
{
    /// <summary>
    /// Ҫ�ٿص��ǰѼ���
    /// </summary>
    public TwineScissor twineScissor;

    protected string HandleOnAnimatorParameter = "TurnOn";
    protected string HandleOffAnimatorParameter = "TurnOff";

    protected override void TurnOn()
    {
        Debug.Log("ScissorHandle On");
        twineScissor.Cut();
    }

    protected override void TurnOff()
    {
        Debug.Log("ScissorHandle off");
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
