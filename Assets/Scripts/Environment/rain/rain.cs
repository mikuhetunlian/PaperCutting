using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    public GameObject FallRain;

    /// <summary>
    /// �����¼� �����������ʱ�򼤻��������
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
