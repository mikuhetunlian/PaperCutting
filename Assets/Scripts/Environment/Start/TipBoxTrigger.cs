using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipBoxTrigger : MonoBehaviour
{
    //�Ƿ񴥷���TipBox�¼�
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
