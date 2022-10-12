using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  RainControlHandle : MonoBehaviour
{
    public GameObject Umbrella;
    public GameObject RainStop;
    protected Animator _animator;
    private bool canBeControl;
    private bool isControlActive;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (canBeControl && !isControlActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                RainStop.GetComponent<rainStopTest>().StopRain();
                _animator.SetBool("on", true);
                isControlActive = true;
                //拧下handle后停止雨伞运动,并且消除雨滴
                Umbrella.GetComponent<PathMovement>().CanMove = false;
                for (int i = 0; i < Umbrella.transform.childCount; i++)
                {
                    GameObject.Destroy(Umbrella.transform.GetChild(i).gameObject);
                }
                //拧下handle后停止销毁折纸桥
                EventMgr.GetInstance().EventTrigger<string>("StopDestoryPaperBridge", "StopDestoryPaperBridge");
                //拧下handle点后触发新的checkPoint
                EventMgr.GetInstance().EventTrigger<string>("RainHandleCheckPointTrigger", "RainHandleCheckPointTrigger");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            canBeControl = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canBeControl = false;
        }
    }

}
