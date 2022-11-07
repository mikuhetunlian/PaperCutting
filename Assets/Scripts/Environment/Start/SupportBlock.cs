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
    /// ȡ���Լ���colliderʹ�÷����ܹ�������
    /// </summary>
    /// <param name="info"></param>
    protected void DestorySupportBlock(string info)
    {
        _collider.enabled = false;
        _animator.SetBool(brokenAnimatorParameter, true);

    }


    /// <summary>
    /// �����Լ���spine�����¼�
    /// </summary>
    public void Destory()
    {
        GameObject.Destroy(this.gameObject);
    }


}
