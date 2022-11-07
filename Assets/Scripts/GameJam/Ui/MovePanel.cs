using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
        EventMgr.GetInstance().AddLinstener<string>("DestroyMovePanel", DestroyMovePanel);
    }

    public override void ShowMe()
    {
        base.ShowMe();
        StartCoroutine(Fade(0, 1));
    }

    protected void DestroyMovePanel(string info)
    {
        StartCoroutine(Fade(1, 0, () =>
        {
            UIMgr.GetInstance().HidePanel<MovePanel>();
        }));
    }


}
