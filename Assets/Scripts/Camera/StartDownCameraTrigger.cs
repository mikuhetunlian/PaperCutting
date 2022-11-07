using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDownCameraTrigger : CameraTriggerBase
{
    protected override void Awake()
    {
        _VCameraName = "StartDownCamera";
        base.Awake();
    }


    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        //��Ϊ  StartDownCamera �Ľ��� ���Զ��л��ģ����Խ�������������������
    }

    protected override void DoWhenTriggerExit(Collider2D collision)
    {
        _exitBlednTime = 2f;
        base.DoWhenTriggerExit(collision);
    }
}
