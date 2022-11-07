using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1CameraTrigger : CameraTriggerBase
{

    protected bool isReadyReleaseScissorTwine = false;

    protected override void Awake()
    {
        _VCameraName = "Room1Camera";
        _inBlendTime = 0.85f;
        _exitBlednTime = 0.85f;
        base.Awake();

        EventMgr.GetInstance().AddLinstener<string>("ReadyReleaseScissorTwine", ReadyReleaseScissorTwine);
    }


    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
        //���׼���ͷŲ�����֦���� ����ٴν�������� ���Ŷ���
        if (isReadyReleaseScissorTwine)
        {
            Invoke("ReleaseScissorTwine", 0.5F);
        }
    }



    protected void ReleaseScissorTwine()
    {
        //�ͷŲ�����֦
        EventMgr.GetInstance().EventTrigger<string>("BrokenBranch", "BrokenBranch");
        //���������������
        EventMgr.GetInstance().EventTrigger<string>("ReleaseScissorTwine", "ReleaseScissorTwine");
        isReadyReleaseScissorTwine = false;
    }



    public void ReadyReleaseScissorTwine(string info)
    {
        isReadyReleaseScissorTwine = true;
    }


}
