using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorCameraTrigger : CameraTriggerBase
{
    protected override void Awake()
    {
        _VCameraName = "CM vcam7";
        base.Awake();
    }

    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
        Debug.Log("½øÈë¼ôµ¶");
    }
}
