using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{

    public Space space = Space.Self;
    public Vector3 RotateSpeed = new Vector3(0,0,10);
    public bool RotateAtStart;

    protected bool _canRotate;

    private void Start()
    {
        Initilization();
    }


    protected void Initilization()
    {
        _canRotate = RotateAtStart ? true : false;
    }
    // Update is called once per frame
    void Update()
    {
        if (_canRotate)
        {
            this.transform.Rotate(RotateSpeed, space);
        }
    }

    public void SetRotate(bool canRotate)
    {
        _canRotate = canRotate;
    }

}
