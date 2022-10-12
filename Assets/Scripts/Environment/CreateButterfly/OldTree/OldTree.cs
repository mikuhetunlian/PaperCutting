using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldTree : MonoBehaviour
{
    ///������Ϸ������
    public GameObject DryerAccessories;
    ///������Ϸ���ʾ
    public DryerUpHint DryerUpHint;
    protected Animator _animator;
    ///�Ƿ񼤻��˴�������
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
    /// ����������� ���� չʾ��ʾ
    /// </summary>
    public void Give()
    {
        DryerAccessories.SetActive(true);
        _isShow = true;
        DryerUpHint.ShowHint();
    }

    /// <summary>
    /// ���ò������ù���ֻ��һ�����
    /// </summary>
    public void ResetAnimatorParameter()
    {
        _animator.SetBool("show", false);
    }


}
