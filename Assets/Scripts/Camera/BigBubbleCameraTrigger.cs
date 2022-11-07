using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBubbleCameraTrigger : CameraTriggerBase
{

    protected override void Awake()
    {
        _VCameraName = "BigBubbleCamera";
        _inBlendTime = 0.8f;
        _exitBlednTime = 1f;
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
