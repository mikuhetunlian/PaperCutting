using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerAccessoriesUp : MonoBehaviour
{
    protected KeepAtPoint _keepAtPoint;
    ///�Ƿ����˼���
    protected bool _isSpeedUp;


    void Start()
    {
        Initilization();
    }

    protected void Initilization()
    {
        _keepAtPoint = GetComponent<KeepAtPoint>();
    }

    private void OnEnable()
    {
        Invoke("AddSpeed", 2f);
    }

    protected void AddSpeed()
    {
        _keepAtPoint.followSpeed = 5;
        _isSpeedUp = true;

    }
}
