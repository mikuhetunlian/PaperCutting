using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingeltonAutoManager<LevelManager>
{
    /// <summary>
    /// �����һ�������ĵص�
    /// </summary>
    public CheckPoint currentCheckPoint;
    /// ͨ�� GameObject.Find ���ҵ�Player
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
    /// ����ɫ��Ҫ��������ʱ������������
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
    /// �޸�Ŀǰ�� checkPoint
    /// </summary>
    /// <param name="checkPoint"></param>
    public void SetCurrentCheckPoint(CheckPoint checkPoint)
    {
        currentCheckPoint = checkPoint;
    }
    
}
