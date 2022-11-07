using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTipTrigger : MonoBehaviour
{

    public List<GameObject> MoveTip;
    ///�ӳٴ�����ʱ��
    public float DelayTime;

    void Start()
    {
        //��Ϸһ��ʼ��ʱһ��󴥷��ƶ���ʾ
        Invoke("TriggerMoveTip", DelayTime);
    }

    /// <summary>
    /// �����ƶ���ʾ
    /// </summary>
    protected void TriggerMoveTip()
    {
        foreach (GameObject obj in MoveTip)
        {
            obj.SetActive(true);
        }
    }
    
}
