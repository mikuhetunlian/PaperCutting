using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PaperScrapCollector : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    //��Ҫ�ռ�����ֽ����
    public short collectNum ;
    //Ŀǰ��ֽ����
    public short currentPapaerNum = 0;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("MeI");
        collectNum = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    /// <summary>
    /// �ռ�����Ʒ��ı�������ɫ ��������������ʵ��
    /// </summary>
    private void CollectAndChangeColor()
    {
        Debug.Log("change color");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("PaperScrap"))
        {
            CollectAndChangeColor();
            currentPapaerNum++;
            if (currentPapaerNum >= collectNum)
            {
                Transfer();
            }
            //��������Ϸ����ǰ�Ȱ�tween�����ˣ���ֹ����Ϊ��
            DOTween.Kill(collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }

    private void Transfer()
    {
  
        _player.transform.position = this.transform.position;
        _player.layer = LayerMask.NameToLayer("mid1");
        _player.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 10);
    }
}