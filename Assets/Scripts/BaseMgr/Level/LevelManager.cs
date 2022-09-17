using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingeltonAutoManager<LevelManager>
{
    /// <summary>
    /// 玩家下一次重生的地点
    /// </summary>
    public CheckPoint currentCheckPoint;
    /// 通过 GameObject.Find 来找到Player
    public Player _player;


    private void Start()
    {
        Initialization();
    }

    protected void Initialization()
    {
        _player = GameObject.FindObjectOfType<Player>();
    }

    /// <summary>
    /// 当角色需要从重生的时候调用这个方法
    /// </summary>
    public void RespawnPlayer()
    {
        if (currentCheckPoint == null)
        {
            return;
        }
        currentCheckPoint.SpawnPlayer(_player);
    }


    /// <summary>
    /// 修改目前的 checkPoint
    /// </summary>
    /// <param name="checkPoint"></param>
    public void SetCurrentCheckPoint(CheckPoint checkPoint)
    {
        currentCheckPoint = checkPoint;
    }
    
}
