using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFlowerCameraTrigger : CameraTriggerBase
{
    protected override void Awake()
    {
        _VCameraName = "BigFlowerCamera";
        _inBlendTime = 1.5f;
        base.Awake();
    }
}
