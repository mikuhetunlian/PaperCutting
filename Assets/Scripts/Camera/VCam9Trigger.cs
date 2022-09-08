using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCam9Trigger : CameraTriggerBase
{

    public Transform effect;
    public Transform to;
    protected override void Awake()
    {
        _VCameraName = "CM vcam9";
        base.Awake();
    }

    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
        ToBeFlower toBeFlower = collision.gameObject.GetComponent<ToBeFlower>();
        toBeFlower.SetPathPoint(effect, to);
    }
}
