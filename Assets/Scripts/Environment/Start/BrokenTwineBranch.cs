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
    /// 树枝脱落
    /// </summary>
    /// <param name="info"></param>
    public void BrokenBranch(string info)
    {
        _animator.SetBool(brokenAnimatorParameter, true);

    }

    /// <summary>
    /// 动画播放完销毁自己 spine动画事件
    /// </summary>
    public void Destory()
    {
        GameObject.Destroy(this.gameObject);
    }

}
