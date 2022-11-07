using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public List<GameObject> scenceList;
    /// <summary>
    /// 被激活的时候重置一次游戏
    /// </summary>
    private void OnEnable()
    {
        ResetGameScence();
    }


    /// <summary>
    /// 重置游戏里的关卡
    /// </summary>
    public void ResetGameScence()
    {
        foreach (GameObject s in scenceList)
        {
            MonoBehaviour[] monos = s.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour mono in monos)
            {
                if (typeof(IScence).IsAssignableFrom(mono.GetType()))
                {
                    IScence scence = mono as IScence;
                    scence.DoResetScence();
                }
               
            }
        }
    }
}
