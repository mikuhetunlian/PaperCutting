using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
        EventMgr.GetInstance().AddLinstener<string>("DestoryJumpPanel", DestoryJumpPanel);
    }


    public override void ShowMe()
    {
        base.ShowMe();
        StartCoroutine(Fade(0, 1));
    }


    public void DestoryJumpPanel(string info)
    {
        StartCoroutine(Fade(1, 0,()=>
        {
            UIMgr.GetInstance().HidePanel<JumpPanel>();
        }));
    }
}
