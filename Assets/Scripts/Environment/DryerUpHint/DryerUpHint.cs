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
    /// 展示吹风机上方提示
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
