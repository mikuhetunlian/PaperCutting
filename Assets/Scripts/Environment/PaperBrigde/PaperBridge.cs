using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PaperBrigdeStartPoint
{
    //代表这个折纸桥的起点坐标
    public Vector3 StartPostion;
}


public class PaperBridge : MonoBehaviour
{


    private string Path = "Prefab/paperBridge";
    public PaperBrigdeStartPoint paperBrigdeStartPoint;
    //折纸桥产生的方向
    public bool isRight;
    //“小桥”的数量
    public short bridgeNum = 8;
    //每个“小桥”产生间隔的时间
    public float deltaTime = 0.05f;

    //下面两个参数都是临时写死的，暂时不具备有可更换美术资源的能力 后面有需要会 再进行优化
    //每个“小桥”之间间隔的距离
    protected float offset_X = 0.8458f * 2;
    /// gameobject 的中心点 与 初始生成 桥的 y 的 距离
    protected float offset_y = 0.84f + 0.45f;
    protected List<GameObject> _papaerBrigdeList;


    private void Awake()
    {
        _papaerBrigdeList = new List<GameObject>();
    }

    /// <summary>
    /// 生成折纸桥
    /// </summary>
    public void CreatePaperBrigde()
    {
        StartCoroutine(DoCreatePaperBridge());
    }

    /// <summary>
    /// 生成折纸桥的协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator DoCreatePaperBridge()
    {

        Debug.Log("开始生成折纸桥");
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
    /// 销毁已经产生的折纸桥
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
