using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyUp : MonoBehaviour
{

    public float speed;

  
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime,Space.World);
    }



}
