using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipBox : MonoBehaviour
{
    public PathMovement movingPlatform;
    public PathMovement door;

    protected Animator _animator;
    protected Pushable _pushable;
    protected Rigidbody2D _rbody;
    //是否trigger了机关
    protected bool _isTriggerMachine;

    void Start()
    {
        Initilization();
    }


    protected void Initilization()
    {
        _animator = GetComponent<Animator>();
        _pushable = GetComponent<Pushable>();
        _pushable.CanBeControl = false;
        _rbody = GetComponent<Rigidbody2D>();

        EventMgr.GetInstance().AddLinstener<string>("ChangeToControlPlatform", ChangeToControlPlatform);
    }
   
    void Update()
    {
        TriggerMachine();
    }

    //触发机关
    protected void TriggerMachine()
    {
        if (_pushable.IsFollowWithMovingPlatform && _pushable.IsFullyTouchMovingPlatform && !_isTriggerMachine)
        {
            movingPlatform.CanMove = true;
            door.CanMove = true;
            _isTriggerMachine = true;
        }
    }

    /// <summary>
    /// 从space 转变到 control
    /// </summary>
    /// <param name="info"></param>
    public void ChangeToControlPlatform(string info)
    {
        _animator.SetBool("change", true);
        _pushable.CanBeControl = true;
    }
}
