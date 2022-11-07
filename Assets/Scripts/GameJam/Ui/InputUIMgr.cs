using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUIMgr : SingeltonAutoManager<InputUIMgr>
{
    ///是否检测某个按键输入
    public bool isDetectButtonInput;
    ///待输入检测的按键
    public KeyCode KeyWaitForInput;
    ///检测完后要触发的事件名
    public string EvnetName;

    // Update is called once per frame
    void Update()
    {
        DetectInput();
    }

    /// <summary>
    /// 当需要检测按键时，进行检测
    /// 目前只支持单击检测
    /// </summary>
    protected void DetectInput()
    {
        if (Input.GetKeyDown(KeyWaitForInput) && isDetectButtonInput)
        {
            isDetectButtonInput = false;
            EventMgr.GetInstance().EventTrigger<string>(EvnetName, EvnetName);
        }
    }

    /// <summary>
    /// 注册待响应按键和响应了按键之后的要触发事件的事件名
    /// </summary>
    /// <param name="key"></param>
    /// <param name="eventName"></param>
    public void SetKeyAndEvent(KeyCode key,string eventName)
    {
        KeyWaitForInput = key;
        EvnetName = eventName;
        isDetectButtonInput = true;
    }
}
