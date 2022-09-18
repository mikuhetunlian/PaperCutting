using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBridge : MonoBehaviour
{


    private string Path = "Prefab/paperBridge";
    //第一个桥开始的位置
    public Vector2 startPos;
    //“小桥”的数量
    public short bridgeNum = 8;
    //每个“小桥”产生间隔的时间
    public float deltaTime = 0.05f;

    //下面两个参数都是临时写死的，暂时不具备有可更换美术资源的能力 后面有需要会 再进行优化
    //每个“小桥”之间间隔的距离
    private float offset_X = 0.8458f * 2;
    /// gameobject 的中心点 与 初始生成 桥的 y 的 距离
    private float offset_y = 0.84f + 0.45f;

    private bool isCreate = false;


    /// <summary>
    /// 生成折纸桥的携程
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreatePaperBridge()
    {

        Debug.Log("开始生成");
        startPos = this.transform.position;
        float X = startPos.x;
        for (int i = 0; i < bridgeNum; i++)
        {
            GameObject bridge = ResMgr.GetInstance().LoadRes<GameObject>(Path);
            bridge.gameObject.transform.position = new Vector2(X, startPos.y - offset_y);
            X += offset_X;
            yield return new WaitForSeconds(deltaTime);
        }      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !isCreate)
        {
            isCreate = true;
            StartCoroutine(CreatePaperBridge());
        }
    }
}
