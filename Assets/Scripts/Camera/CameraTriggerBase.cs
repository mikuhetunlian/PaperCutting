using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTriggerBase : MonoBehaviour
{
    protected string _VCameraName;
    protected CinemachineVirtualCamera _VCamera;
    protected float _inBlendTime = 2;
    protected float _exitBlednTime = 2;

    /// <summary>
    /// 必须要在base()之前 重写 _VCameraName 
    /// 如果有需要，要在awake中重写 _inBlendTime 和  _exitBlednTime；默认值分别是 2 和 2
    /// </summary>
    protected virtual void Awake()
    {
        _VCamera = GameObject.Find(_VCameraName).GetComponent<CinemachineVirtualCamera>();
    }

    protected virtual void Update()
    {
        
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            DoWhenTriggerEnter(collision);
        }
    }

    protected virtual void DoWhenTriggerEnter(Collider2D collision)
    {
        _VCamera.enabled = true;
        CameraMgr.GetInstance().SetDefaultBlednTime(_inBlendTime);
        CameraMgr.GetInstance().SetCamerasSize();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            DoWhenTriggerExit(collision);
        }
    }

    /// <summary>
    /// 默认离开的时候不会重置摄像机们的size的偏移
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void DoWhenTriggerExit(Collider2D collision)
    {
        _VCamera.enabled = false;
        CameraMgr.GetInstance().SetDefaultBlednTime(_exitBlednTime);

    }


   




}
