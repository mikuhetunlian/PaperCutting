using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public Player.FacingDirections FacingDirection  = Player.FacingDirections.Right;

    protected List<Respawnable> _listener;


    private void Awake()
    {
        _listener = new List<Respawnable>();
    }


    /// <summary>
    /// 重生想要重生的player ,由 LevelManager 调用
    /// </summary>
    /// <param name="player"></param>
    public virtual void SpawnPlayer(Player player)
    {
        player.RespawnAt(transform, FacingDirection);
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
