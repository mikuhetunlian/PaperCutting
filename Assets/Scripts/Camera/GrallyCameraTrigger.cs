using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrallyCameraTrigger : CameraTriggerBase
{

    protected override void Awake()
    {
        _VCameraName = "grallyCamera";
        _inBlendTime = 1f;
        _exitBlednTime = 1f;
        base.Awake();
    }


    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
    }

}
