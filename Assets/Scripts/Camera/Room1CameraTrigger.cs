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
        //如果准备释放缠绕树枝并且 玩家再次进入该区域 播放动画
        if (isReadyReleaseScissorTwine)
        {
            Invoke("ReleaseScissorTwine", 0.5F);
        }
    }



    protected void ReleaseScissorTwine()
    {
        //释放缠绕树枝
        EventMgr.GetInstance().EventTrigger<string>("BrokenBranch", "BrokenBranch");
        //解除剪刀缠绕限制
        EventMgr.GetInstance().EventTrigger<string>("ReleaseScissorTwine", "ReleaseScissorTwine");
        isReadyReleaseScissorTwine = false;
    }



    public void ReadyReleaseScissorTwine(string info)
    {
        isReadyReleaseScissorTwine = true;
    }


}
