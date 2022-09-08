using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBridge : MonoBehaviour
{

    private string Path = "Prefab/paperBridge2";
    //��һ���ſ�ʼ��λ��
    public Vector2 startPos = new Vector2(294.7f,-6.17f);
    //��С�š�������
    public short bridgeNum = 8;
    //ÿ����С�š����������ʱ��
    public float deltaTime = 0.1f;
    //ÿ����С�š�֮�����ľ���
    private float offset_X = 0.8458f * 2;
    private bool isCreate;


    private IEnumerator CreatePaperBridge()
    {
        Debug.Log("��ʼ����");
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
