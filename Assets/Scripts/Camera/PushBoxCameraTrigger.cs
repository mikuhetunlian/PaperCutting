using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxCameraTrigger : CameraTriggerBase
{


    protected override void Awake()
    {
        _VCameraName = "pushBoxCamera";
        _inBlendTime = 0.7f;
        _exitBlednTime = 1f;
        base.Awake();
    }


}
