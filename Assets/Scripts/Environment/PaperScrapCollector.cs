using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PaperScrapCollector : MonoBehaviour
{

    //需要收集到的纸碎数
    public short collectNum ;
    //目前的纸碎数
    public short currentPapaerNum = 0;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("MeI");
        collectNum = 3;
    }


    /// <summary>
    /// 收集到物品后改变自已颜色 或许交给动画来实现
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
            //在销毁游戏对象前先把tween销毁了，防止引用为空
            DOTween.Kill(collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }

    private void Transfer()
    {
        _player.transform.position = this.transform.position;
        _player.GetComponent<Player>().ToVisiable();
        _player.GetComponent<PlayerController>().SetVerticalForce(10f);
    }
}
