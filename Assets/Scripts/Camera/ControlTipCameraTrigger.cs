using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTipCameraTrigger : CameraTriggerBase
{
    protected override void Awake()
    {
        _VCameraName = "ControlCamera";
        base.Awake();
    }
}
