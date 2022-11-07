using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControlPanel : BasePanel
{


    protected override void Awake()
    {
        base.Awake();
        EventMgr.GetInstance().AddLinstener<string>("DestoryControlPanel", DestoryControlPanel);
    }

    public override void ShowMe()
    {
        base.ShowMe();
        StartCoroutine(Fade(0, 1));
    }


    protected void DestoryControlPanel(string info)
    {
        Dictionary<string,BasePanel> dic =  UIMgr.GetInstance().panelDic;
        StartCoroutine(Fade(1, 0, () =>
        {
            UIMgr.GetInstance().HidePanel<ControlPanel>();
        }));
    }

   
}
