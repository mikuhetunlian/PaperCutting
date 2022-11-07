using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBridgeHandle : MonoBehaviour
{
    private List<PaperBridge> paperBrigdeList;
    private bool hasCreate;
    private Animator _animator;

    void Start()
    {
        paperBrigdeList = new List<PaperBridge>();
        for (int i = 0; i < transform.childCount; i++)
        {
            PaperBridge p = transform.GetChild(i).GetComponent<PaperBridge>();
            paperBrigdeList.Add(p);
        }
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 改变handle的状态
    /// </summary>
    protected void ChangeHandleState()
    {
        _animator.SetBool("change", true);
        hasCreate = true;
    }

    /// <summary>
    /// 产生折纸桥
    /// </summary>
    public void CreateBrigde()
    {
        foreach (PaperBridge p in paperBrigdeList)
        {
            p.CreatePaperBrigde();
        }
        hasCreate = true;
    }

    /// <summary>
    /// 当玩家死亡，并且没拉下停雨柄的时候，重置已经产生折纸桥
    /// </summary>
    public void ResetBrigde()
    {
        foreach (PaperBridge p in paperBrigdeList)
        {
            p.DestoryPaperBridge();
            _animator.SetBool("change", false);
            hasCreate = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (InputManager.GetInstance().ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonDown &&
                !hasCreate)
            {
                ChangeHandleState();
            }
        }
    }


}
