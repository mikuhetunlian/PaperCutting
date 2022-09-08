using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ferris : MonoBehaviour
{
    public float rotateSpeed;

    void Start()
    {
        rotateSpeed = -45;    
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime, Space.World);
    }
}
