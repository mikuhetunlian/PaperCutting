using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyUpRoot : MonoBehaviour
{
    protected List<GameObject> butterflies = new List<GameObject>();
    
    void Start()
    {
        GetButterflies();
        EventMgr.GetInstance().AddLinstener<float>("ActiveButterfly", ActiveButterfly);
    }


    /// <summary>
    /// ��� root �µĺ���
    /// </summary>
    protected void GetButterflies()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            butterflies.Add(transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// ���������������򣬼��������
    /// </summary>
    public void ActiveButterfly(float delayTime)
    {
        StartCoroutine(ActiveButterflyCoroutine(delayTime));
    }

    protected IEnumerator ActiveButterflyCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        foreach (GameObject butterfly in butterflies)
        {
            butterfly.SetActive(true);
        }
    }








}
