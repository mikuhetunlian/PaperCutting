using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_3 : MonoBehaviour
{
    protected Jump _jump;
    protected PlayerController _playerController;
    
    void Start()
    {
        MusicMgr.GetInstance().PlayMusicEffect("touchGround", false);
        Debug.Log("²¥·Åtouochground");
        _playerController = GetComponent<PlayerController>();
        _jump = GetComponent<Jump>();
        _jump.JumpHeight = 15;
        UIMgr.GetInstance().ShowPanel<JumpPanel>();
        InputUIMgr.GetInstance().SetKeyAndEvent(KeyCode.Space, "DestoryJumpPanel");
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerController.Parameters.Gravity != -100)
        {
            _playerController.Parameters.Gravity = -100;
        }
        if (_playerController.RayOffsetHorizontal != 0.5f)
        {
            _playerController.RayOffsetHorizontal = 0.5f;
        }
    }
}
