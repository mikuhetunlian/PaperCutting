using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTwineBranch : MonoBehaviour
{
    protected Animator _animator;
    protected string brokenAnimatorParameter = "broken";

    void Start()
    {
        _animator = GetComponent<Animator>();
        EventMgr.GetInstance().AddLinstener<string>("BrokenBranch", BrokenBranch);
    }

  
    /// <summary>
    /// ��֦����
    /// </summary>
    /// <param name="info"></param>
    public void BrokenBranch(string info)
    {
        _animator.SetBool(brokenAnimatorParameter, true);

    }

    /// <summary>
    /// ���������������Լ� spine�����¼�
    /// </summary>
    public void Destory()
    {
        GameObject.Destroy(this.gameObject);
    }

}
