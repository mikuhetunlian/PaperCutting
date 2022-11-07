using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCameraTrigger : CameraTriggerBase
{

    protected override void Awake()
    {
        _VCameraName = "WheelCamera";
        _inBlendTime = 1.5f;
        _exitBlednTime = 1.5f;
        base.Awake();
    }

}
