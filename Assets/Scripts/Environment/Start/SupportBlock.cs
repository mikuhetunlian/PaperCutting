using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportBlock : MonoBehaviour
{
    protected BoxCollider2D _collider;
    protected Animator _animator;
    protected string brokenAnimatorParameter = "broken";


    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        EventMgr.GetInstance().AddLinstener<string>("DestorySupportBlock", DestorySupportBlock);
    }

    /// <summary>
    /// 取消自己的collider使得方块能够掉下来
    /// </summary>
    /// <param name="info"></param>
    protected void DestorySupportBlock(string info)
    {
        _collider.enabled = false;
        _animator.SetBool(brokenAnimatorParameter, true);

    }


    /// <summary>
    /// 销毁自己，spine动画事件
    /// </summary>
    public void Destory()
    {
        GameObject.Destroy(this.gameObject);
    }


}
