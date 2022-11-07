using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scence1 : MonoBehaviour,IScence
{
    /// <summary>
    /// 场景中需要reest的物体
    /// </summary>
    public List<GameObject> ResetObjList;
    

    public void DoResetScence()
    {
        foreach (GameObject obj in ResetObjList)
        {
            MonoBehaviour[] monos = obj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour mono in monos)
            {
                if (typeof(IResetAble).IsAssignableFrom(mono.GetType()))
                {
                    if (!mono.gameObject.activeInHierarchy)
                    {
                        mono.gameObject.SetActive(true);
                    }
                    IResetAble reset = mono as IResetAble;
                    reset.Reset();
                }
            }
        }
    }



}
