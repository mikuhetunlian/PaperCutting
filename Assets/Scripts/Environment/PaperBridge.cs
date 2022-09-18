using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBridge : MonoBehaviour
{


    private string Path = "Prefab/paperBridge";
    //��һ���ſ�ʼ��λ��
    public Vector2 startPos;
    //��С�š�������
    public short bridgeNum = 8;
    //ÿ����С�š����������ʱ��
    public float deltaTime = 0.05f;

    //������������������ʱд���ģ���ʱ���߱��пɸ���������Դ������ ��������Ҫ�� �ٽ����Ż�
    //ÿ����С�š�֮�����ľ���
    private float offset_X = 0.8458f * 2;
    /// gameobject �����ĵ� �� ��ʼ���� �ŵ� y �� ����
    private float offset_y = 0.84f + 0.45f;

    private bool isCreate = false;


    /// <summary>
    /// ������ֽ�ŵ�Я��
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreatePaperBridge()
    {

        Debug.Log("��ʼ����");
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
