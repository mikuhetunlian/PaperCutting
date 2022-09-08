using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetPanel : BasePanel
{
    public override void ShowMe()
    {
        base.ShowMe();
    }

    public override void HideMe()
    {
        base.HideMe();
    }


    protected override void TextOnClick()
    {
        for (int i = 0; i < textList.Count; i++)
        {
            switch (textList[i].gameObject.name)
            {
                case "txtMusic":
                    UIMgr.AddCustomEventTrigger(textList[i], EventTriggerType.PointerClick, (data) =>
                    {
                        
                        UIMgr.GetInstance().ShowPanel<MusicPanel>();
                        UIMgr.GetInstance().HidePanel<SetPanel>();
                    });
                    break;
                case "txtKey":
                    UIMgr.AddCustomEventTrigger(textList[i], EventTriggerType.PointerClick, (data) =>
                    {
                        
                        UIMgr.GetInstance().ShowPanel<KeyPanel>((panel) =>
                        {
                           
                        });
                        UIMgr.GetInstance().HidePanel<SetPanel>();
                    });

                    break;
                case "txtExit":
                    UIMgr.AddCustomEventTrigger(textList[i], EventTriggerType.PointerClick, (data) =>
                    {
                        UIMgr.GetInstance().ShowPanel<StartPanel>();
                        UIMgr.GetInstance().HidePanel<SetPanel>();
                    });
                    break;
            }
        }
    }


  
}
