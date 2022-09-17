using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{

    public int DamageCaused = 5;
    protected Health _collsionHealth;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (_collsionHealth == null)
            {
                _collsionHealth = collision.gameObject.GetComponent<Health>();
            }
            _collsionHealth.Damage(DamageCaused);
        }
    }






}
