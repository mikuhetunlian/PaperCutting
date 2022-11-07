using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Trigger : CameraTriggerBase
{
    /// <summary>
    /// 第一次进入trigger需要激活的列表
    /// </summary>
    public List<GameObject> ActiveList;
    protected bool isActiveArrow;
    protected override void Awake()
    {
        _VCameraName = "Camera2";
        _inBlendTime = 0.6f;
        _exitBlednTime = 1.5f;
        base.Awake();
    }

    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
        if (!isActiveArrow)
        {
            isActiveArrow = true;
            Invoke("Active", 1);
        }
    }

    protected void  Active()
    {
        foreach (GameObject obj in ActiveList)
        {
            obj.SetActive(true);
        }
    }
}
