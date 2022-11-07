using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTipTrigger : MonoBehaviour
{

    public List<GameObject> MoveTip;
    ///延迟触发的时间
    public float DelayTime;

    void Start()
    {
        //游戏一开始延时一会后触发移动提示
        Invoke("TriggerMoveTip", DelayTime);
    }

    /// <summary>
    /// 触发移动提示
    /// </summary>
    protected void TriggerMoveTip()
    {
        foreach (GameObject obj in MoveTip)
        {
            obj.SetActive(true);
        }
    }
    
}
