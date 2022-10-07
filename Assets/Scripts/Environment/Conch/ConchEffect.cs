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
    /// ���� conch �ű����¼�����
    /// </summary>
    /// <param name="info"></param>
    public void ShowConchEffect(string info)
    {
        _animator.SetBool("show",true);
    }

    /// <summary>
    /// �����¼� ���ò���
    /// </summary>
    public void ResetAnimatorParameter()
    {
        _animator.SetBool("show",false);
    }
}
