using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCam10Trigger : CameraTriggerBase
{
    protected override void Awake()
    {
        _VCameraName = "CM vcam10";
        base.Awake();
    }

    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
  
        base.DoWhenTriggerEnter(collision);
        //ChangeSkin changeSkin = collision.gameObject.GetComponent<ChangeSkin>();
        //changeSkin.ChangeSkeletonSkin("shadowPlay");
    }




}
