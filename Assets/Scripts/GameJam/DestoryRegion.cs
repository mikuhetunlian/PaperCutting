using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryRegion : MonoBehaviour
{
    public Flow flow;
    protected bool isBroken;

    private void Start()
    {
        EventMgr.GetInstance().AddLinstener<string>("ResetIsBroken", ResetIsBroken);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && flow.isFlow && !isBroken)
        {
            EventMgr.GetInstance().EventTrigger<string>("Broken", "Broken");
            isBroken = true;
        }
    }

    public void ResetIsBroken(string info)
    {
        Debug.Log("ResetIsBroken");
        isBroken = false;
    }
}
