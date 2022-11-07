using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public List<GameObject> scenceList;
    /// <summary>
    /// �������ʱ������һ����Ϸ
    /// </summary>
    private void OnEnable()
    {
        ResetGameScence();
    }


    /// <summary>
    /// ������Ϸ��Ĺؿ�
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
