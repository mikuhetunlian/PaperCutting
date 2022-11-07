using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonCameraTrigger : CameraTriggerBase
{
    public List<GameObject> ActiveList;

    protected override void Awake()
    {
        _VCameraName = "ionCamera";
        base.Awake();
    }

    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
        foreach (GameObject obj in ActiveList)
        {
            obj.SetActive(true);
        }
    }
}
