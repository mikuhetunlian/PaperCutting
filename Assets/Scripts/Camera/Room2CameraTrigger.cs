using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2CameraTrigger : CameraTriggerBase
{
    protected override void Awake()
    {
        _VCameraName = "Room2Camera";
        _inBlendTime = 0.85f;
        _exitBlednTime = 0.85f;
        base.Awake();
    }
}
