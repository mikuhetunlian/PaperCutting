using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicPanel : BasePanel
{

    protected override void Awake()
    {
        base.Awake();
        FindChildrenControls<Slider>();
    }
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
                case "txtBack":
                    UIMgr.AddCustomEventTrigger(textList[i], EventTriggerType.PointerClick, (data) =>
                    {
                        Debug.Log("txtBack");
                        UIMgr.GetInstance().ShowPanel<SetPanel>();
                        UIMgr.GetInstance().HidePanel<MusicPanel>();
                    });
                    break;
                
            }
        }
    }




    protected override void OnValueChange(string name, float value)
    {
        switch (name)
        {
            case "SliderMainVolume":
               
                break;
            case "SliderBKVolume":
              
                break;
            case "SliderSoundVolume":
               
                break;
        }
    }
}
