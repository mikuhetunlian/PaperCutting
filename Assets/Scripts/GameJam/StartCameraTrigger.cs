using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraTrigger : CameraTriggerBase
{
    protected override void Awake()
    {
        _VCameraName = "StartCamera";
        base.Awake();
    }
}
