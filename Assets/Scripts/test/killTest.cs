using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class killTest : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.Damage(10);
        }
    }
}