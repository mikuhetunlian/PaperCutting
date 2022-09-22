using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    public GameObject FallRain;

    /// <summary>
    /// 动画事件 当下雨结束的时候激活下落雨滴
    /// </summary>
    public void ActiveRainFall()
    {
        if (FallRain == null || FallRain.activeInHierarchy)
        {
            return;
        }
        MusicMgr.GetInstance().StopBkMusic();
        FallRain.SetActive(true);
        Debug.Log("ActiveRainFall desu");
    }
}
