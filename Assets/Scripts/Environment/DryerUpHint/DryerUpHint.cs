using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerUpHint : MonoBehaviour
{

    protected Animator _animator;
    protected bool _isSHowHint;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// չʾ������Ϸ���ʾ
    /// </summary>
    public void ShowHint()
    {
        if (!_isSHowHint)
        {
            _animator.SetBool("show", true);
            _isSHowHint = true;
        }
      
    }

}
