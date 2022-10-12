using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PaperBrigdeStartPoint
{
    //���������ֽ�ŵ��������
    public Vector3 StartPostion;
}


public class PaperBridge : MonoBehaviour
{


    private string Path = "Prefab/paperBridge";
    public PaperBrigdeStartPoint paperBrigdeStartPoint;
    //��ֽ�Ų����ķ���
    public bool isRight;
    //��С�š�������
    public short bridgeNum = 8;
    //ÿ����С�š����������ʱ��
    public float deltaTime = 0.05f;

    //������������������ʱд���ģ���ʱ���߱��пɸ���������Դ������ ��������Ҫ�� �ٽ����Ż�
    //ÿ����С�š�֮�����ľ���
    protected float offset_X = 0.8458f * 2;
    /// gameobject �����ĵ� �� ��ʼ���� �ŵ� y �� ����
    protected float offset_y = 0.84f + 0.45f;
    protected List<GameObject> _papaerBrigdeList;


    private void Awake()
    {
        _papaerBrigdeList = new List<GameObject>();
    }

    /// <summary>
    /// ������ֽ��
    /// </summary>
    public void CreatePaperBrigde()
    {
        StartCoroutine(DoCreatePaperBridge());
    }

    /// <summary>
    /// ������ֽ�ŵ�Э��
    /// </summary>
    /// <returns></returns>
    private IEnumerator DoCreatePaperBridge()
    {

        Debug.Log("��ʼ������ֽ��");
        _papaerBrigdeList.Clear();
        float X = paperBrigdeStartPoint.StartPostion.x;
        for (int i = 0; i < bridgeNum; i++)
        {
            GameObject bridge = ResMgr.GetInstance().LoadRes<GameObject>(Path);
            _papaerBrigdeList.Add(bridge);
            bridge.gameObject.transform.position = new Vector2(X, paperBrigdeStartPoint.StartPostion.y - offset_y);

            if (isRight)
            {
                X += offset_X;
            }
            else
            {
                X -= offset_X;
            }
            yield return new WaitForSeconds(deltaTime);
        }      
    }


    /// <summary>
    /// �����Ѿ���������ֽ��
    /// </summary>
    public void DestoryPaperBridge()
    {
        foreach (GameObject p in _papaerBrigdeList)
        {
            GameObject.Destroy(p);
        }
    }

 
    public Vector3 GetStartPositon()
    {
        return paperBrigdeStartPoint.StartPostion;
    }
}
