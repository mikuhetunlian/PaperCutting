using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
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
                case "txtStart":
                    UIMgr.AddCustomEventTrigger(textList[i], EventTriggerType.PointerClick, (data) =>
                     {
                          Debug.Log("txtStart");
                          SceneMgr.GetInstance().LoadSceneAsync("testScene");
                          UIMgr.GetInstance().HidePanel<BKPanel>();
                          UIMgr.GetInstance().HidePanel<StartPanel>();
                          
                     });
                    //Animator animator = textList[i].gameObject.GetComponent<Animator>();
                    //animator.SetBool("isShow", true);
                    break;
                case "txtSet":
                    UIMgr.AddCustomEventTrigger(textList[i], EventTriggerType.PointerClick, (data) =>
                    {
                        Debug.Log("txtSet");
                        UIMgr.GetInstance().ShowPanel<SetPanel>((panel) =>
                        {
                            Debug.Log("执行" + panel.name + "回调");
                        });
                        UIMgr.GetInstance().HidePanel<StartPanel>();
                    });
                   
                    break;
                case "txtExit":
                    UIMgr.AddCustomEventTrigger(textList[i], EventTriggerType.PointerClick, (data) =>
                    {
                        Debug.Log("txtExit");
                        //在编辑模式下没有用，正式打包出去后才有用
                        Application.Quit();
                    });
                    break;
            }
        }
    }





}
