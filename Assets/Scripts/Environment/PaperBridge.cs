using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBridge : MonoBehaviour
{

    private string Path = "Prefab/paperBridge2";
    //第一个桥开始的位置
    public Vector2 startPos = new Vector2(294.7f,-6.17f);
    //“小桥”的数量
    public short bridgeNum = 8;
    //每个“小桥”产生间隔的时间
    public float deltaTime = 0.1f;
    //每个“小桥”之间间隔的距离
    private float offset_X = 0.8458f * 2;
    private bool isCreate;


    private IEnumerator CreatePaperBridge()
    {
        Debug.Log("开始生成");
        float X = startPos.x;
        for (int i = 0; i < bridgeNum; i++)
        {
            GameObject bridge = ResMgr.GetInstance().LoadRes<GameObject>(Path);
            bridge.gameObject.transform.position = new Vector2(X, startPos.y);
            X += offset_X;
            yield return new WaitForSeconds(deltaTime);
        }
        isCreate = true;
      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !isCreate)
        {
            Debug.Log("player in");
            StartCoroutine(CreatePaperBridge());
        }
    }
}
