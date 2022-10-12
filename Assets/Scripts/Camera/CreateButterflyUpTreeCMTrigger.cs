using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateButterflyUpTreeCMTrigger : CameraTriggerBase
{

    protected override void Awake()
    {
        _VCameraName = "CM vcam13";
        base.Awake();
    }

    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
    }
    protected override void DoWhenTriggerExit(Collider2D collision)
    {
        base.DoWhenTriggerExit(collision);
    }

}
