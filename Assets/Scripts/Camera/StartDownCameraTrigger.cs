using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDownCameraTrigger : CameraTriggerBase
{
    protected override void Awake()
    {
        _VCameraName = "StartDownCamera";
        base.Awake();
    }


    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        //因为  StartDownCamera 的进入 是自动切换的，所以进入区域这个激活不用做事
    }

    protected override void DoWhenTriggerExit(Collider2D collision)
    {
        _exitBlednTime = 2f;
        base.DoWhenTriggerExit(collision);
    }
}
