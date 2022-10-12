using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldTree : MonoBehaviour
{
    ///吹风机上方的配件
    public GameObject DryerAccessories;
    ///吹风机上方提示
    public DryerUpHint DryerUpHint;
    protected Animator _animator;
    ///是否激活了吹风机配件
    protected bool _isShow;



    private void Start()
    {
        Initilization();
    }

    protected void Initilization()
    {
        _animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !_isShow)
        {
            _animator.SetBool("show", true);
        }
    }

    /// <summary>
    /// 古树给出配件 并且 展示提示
    /// </summary>
    public void Give()
    {
        DryerAccessories.SetActive(true);
        _isShow = true;
        DryerUpHint.ShowHint();
    }

    /// <summary>
    /// 重置参数，让古树只给一次配件
    /// </summary>
    public void ResetAnimatorParameter()
    {
        _animator.SetBool("show", false);
    }


}
