using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPassRegion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            EventMgr.GetInstance().EventTrigger<string>("DestoryFlow", "DestoryFlow");
            GameObject.Destroy(this.gameObject);
        }

    }


}
