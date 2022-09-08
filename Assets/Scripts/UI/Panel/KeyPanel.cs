using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyPanel : BasePanel
{

    protected override void TextOnClick()
    {
        for (int i = 0; i < textList.Count; i++)
        {
            switch (textList[i].gameObject.name)
            {
                case "txtBack":
                    UIMgr.AddCustomEventTrigger(textList[i], EventTriggerType.PointerClick, (data) =>
                    {
                        Debug.Log("txtBack");
                        UIMgr.GetInstance().ShowPanel<SetPanel>();
                        UIMgr.GetInstance().HidePanel<KeyPanel>();
                    });
                    break;

            }
        }
    }
}
