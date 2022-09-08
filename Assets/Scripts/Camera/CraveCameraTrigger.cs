using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CraveCameraTrigger : CameraTriggerBase
{

    ///进入洞穴时的水平移动速度
    public float playerInSpeed;
    ///离开洞穴后的水平移动速度
    public float playerOutSpeed;


    protected override void Awake()
    {
        _VCameraName = "CM vcam6";
        _inBlendTime = 2.5f;
        _exitBlednTime = 2;
        playerInSpeed = 6;
        playerOutSpeed = 16;
        base.Awake();
    }

    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
        HorizontalMove horizontalMove = collision.GetComponent<HorizontalMove>();
        horizontalMove.speed = playerInSpeed;
    }


    protected override void DoWhenTriggerExit(Collider2D collision)
    {
        base.DoWhenTriggerExit(collision);
        HorizontalMove horizontalMove = collision.GetComponent<HorizontalMove>();
        horizontalMove.speed = playerOutSpeed;
    }

    //protected virtual void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name.Equals("MeI"))
    //    {
    //        HorizontalMove horizontalMove = collision.gameObject.GetComponent<HorizontalMove>();
    //        horizontalMove.speed = 6;

    //        CameraMgr.GetInstance().SetDefaultBlednTime(2.5f);
    //        _VCamera.enabled = true;
    //        CameraMgr.GetInstance().SetCamerasSize();
    //    }
    //}


    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name.Equals("MeI"))
    //    {
    //        HorizontalMove horizontalMove = collision.gameObject.GetComponent<HorizontalMove>();
    //        horizontalMove.speed = 16;

    //        Debug.Log("离开");
    //        CameraMgr.GetInstance().SetDefaultBlednTime(2);
    //        _VCamera.enabled = false;
    //        CameraMgr.GetInstance().ResetCameraSize();
    //    }
    //}
}
