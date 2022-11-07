using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipBoxTrigger : MonoBehaviour
{
    //是否触发了TipBox事件
    protected bool isTriggerEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !isTriggerEvent)
        {
            EventMgr.GetInstance().EventTrigger<string>("ChangeToControlPlatform", "ChangeToControlPlatform");
            isTriggerEvent = true;
        }
    }
}
