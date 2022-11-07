using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUIMgr : SingeltonAutoManager<InputUIMgr>
{
    ///�Ƿ���ĳ����������
    public bool isDetectButtonInput;
    ///��������İ���
    public KeyCode KeyWaitForInput;
    ///������Ҫ�������¼���
    public string EvnetName;

    // Update is called once per frame
    void Update()
    {
        DetectInput();
    }

    /// <summary>
    /// ����Ҫ��ⰴ��ʱ�����м��
    /// Ŀǰֻ֧�ֵ������
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
    /// ע�����Ӧ��������Ӧ�˰���֮���Ҫ�����¼����¼���
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
