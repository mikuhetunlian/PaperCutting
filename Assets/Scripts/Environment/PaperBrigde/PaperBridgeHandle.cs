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


    protected void ChangeHandleState()
    {
        _animator.SetBool("change", true);
        hasCreate = true;
    }


    public void CreateBrigde()
    {
        foreach (PaperBridge p in paperBrigdeList)
        {
            p.CreatePaperBrigde();
        }
        hasCreate = true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && !hasCreate)
            {
                ChangeHandleState();
            }
        }
    }


}
