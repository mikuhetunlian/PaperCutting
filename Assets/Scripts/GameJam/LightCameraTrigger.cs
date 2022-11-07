using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCameraTrigger : CameraTriggerBase
{
    protected override void Awake()
    {
        _VCameraName = "LightCamera";
        _inBlendTime = 1f;
        base.Awake();
    }
}
