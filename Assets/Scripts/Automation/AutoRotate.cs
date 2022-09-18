using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{

    public Space space = Space.Self;
    public float speedMutiper = 100;
    public Vector3 RotateSpeed = new Vector3(0,0,500);
    
     
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(RotateSpeed, space);
    }
}
