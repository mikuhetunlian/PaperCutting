using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_4 : MonoBehaviour
{
    public Transform flowCheckpoint;
    protected HorizontalMove _horizontalMove;
    protected Jump _jump;
    protected PlayerController _playerController;
    protected Animator _animator;

    void Start()
    {
        EventMgr.GetInstance().AddLinstener<string>("Broken",DoBroken);
        _horizontalMove = GetComponent<HorizontalMove>();
        _jump = GetComponent<Jump>();
        _animator = GetComponent<Animator>();
        _horizontalMove.speed = 15;
        _jump.JumpHeight = 13;
        _playerController = GetComponent<PlayerController>();
        _playerController.RayOffsetHorizontal = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerController.RayOffsetHorizontal != 1f)
        {
            _playerController.RayOffsetHorizontal = 1f;
        }
        if (_horizontalMove.speed != 25)
        {
            _horizontalMove.speed = 25;
        }
        if (_playerController.Parameters.Gravity != -130)
        {
            _playerController.Parameters.Gravity = -130;
        }
        if (_jump.JumpHeight != 18)
        {
            _jump.JumpHeight = 18;
        }

    }

    public void DoBroken(string info)
    {
        _animator.SetBool("broken", true);
        
    }

    /// <summary>
    /// ¶¯»­ÊÂ¼þ
    /// </summary>
    public void BrokenEvent()
    {
        Debug.Log("BrokenEvent");
        _animator.SetBool("broken", false);
        this.transform.position = flowCheckpoint.position;
        EventMgr.GetInstance().EventTrigger("ResetIsBroken", "ResetIsBroken");
    }
    
}
