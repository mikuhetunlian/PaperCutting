using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{




    ///目前的血量
    [ReadOnly]
    public int CurrentHealth;

    ///初始血量
    [Header("Health")]
    public int InitiaHeath;
    ///最大血量
    public int MaximumHealth;


    private void Start()
    {
        Initialization();
    }

    public void Initialization()
    {
        CurrentHealth = 10;
        InitiaHeath = 10;
        MaximumHealth = 10;
    }


    /// <summary>
    /// 当被机关触碰到时，调用这个函数来减少player的血量
    /// </summary>
    public void Damage(int damge)
    {
        if (CurrentHealth < 0)
        {
            return;
        }
        CurrentHealth -= damge;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Kill();
        }
    }


    /// <summary>
    /// 当血量<0时，执行kill函数
    /// </summary>
    public void Kill()
    {
        Debug.Log("player kill");
        LevelManager.GetInstance().RespawnPlayer();
        ResetHealth();
    }

    /// <summary>
    /// 重置血量
    /// </summary>
    public void ResetHealth()
    {
        CurrentHealth = MaximumHealth;
    }

}
