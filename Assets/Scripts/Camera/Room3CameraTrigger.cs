using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3CameraTrigger : CameraTriggerBase
{
    protected override void Awake()
    {
        _VCameraName = "Room3Camera";
        _inBlendTime = 1;
        _exitBlednTime = 1;
        base.Awake();
    }
}
