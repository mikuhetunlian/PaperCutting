using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomoCameraTrigger : CameraTriggerBase
{

    public  GameObject momoTree1;
    public GameObject momoTree2;

    private bool isActiveMomoTree;
    protected override void Awake()
    {
        _VCameraName = "CM vcam7";
        base.Awake();
    }

    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
        if (!isActiveMomoTree)
        {
            momoTree1.SetActive(true);
            momoTree2.SetActive(true);
        }
    }




}
