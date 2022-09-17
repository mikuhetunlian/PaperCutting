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
    /// ������Ҫ������player ,�� LevelManager ����
    /// </summary>
    /// <param name="player"></param>
    public virtual void SpawnPlayer(Player player)
    {
        player.RespawnAt(transform, FacingDirection);
    }




    /// <summary>
    /// ��ҽ��� checkPoint ���� LevelManager�е� currentCheckPoint
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
