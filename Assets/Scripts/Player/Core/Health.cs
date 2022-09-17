using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{




    ///Ŀǰ��Ѫ��
    [ReadOnly]
    public int CurrentHealth;

    ///��ʼѪ��
    [Header("Health")]
    public int InitiaHeath;
    ///���Ѫ��
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
    /// �������ش�����ʱ�������������������player��Ѫ��
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
    /// ��Ѫ��<0ʱ��ִ��kill����
    /// </summary>
    public void Kill()
    {
        Debug.Log("player kill");
        LevelManager.GetInstance().RespawnPlayer();
        ResetHealth();
    }

    /// <summary>
    /// ����Ѫ��
    /// </summary>
    public void ResetHealth()
    {
        CurrentHealth = MaximumHealth;
    }

}
