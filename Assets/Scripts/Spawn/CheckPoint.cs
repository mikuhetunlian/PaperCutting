using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public CheckPointData checkPointData;

    private void Awake()
    {
        checkPointData = new CheckPointData();
        checkPointData.checkPointName = this.gameObject.name;
    }


    /// <summary>
    /// 重生想要重生的player ,由 LevelManager 调用
    /// </summary>
    /// <param name="player"></param>
    public virtual void SpawnPlayer(Player player)
    {
        player.RespawnAt(this.transform, checkPointData.facingDirection);
    }




    /// <summary>
    /// 玩家进入 checkPoint 更新 LevelManager中的 currentCheckPoint
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            LevelManager.GetInstance().SetCurrentCheckPoint(this);
        }
    }

}
