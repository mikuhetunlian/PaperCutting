using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DryerDonwEffectCMTrigger : CameraTriggerBase
{

    ///到轨道终点的时候 摄像头要缩放到的大小
    public float TargetLenSize;

    protected float _originLenSize;
    protected CinemachineSmoothPath _path;
    protected CinemachineTrackedDolly _trackDolly;
    protected bool _resetCamera;
    protected string _eventName = "ChangeToGreateCamera";

    protected override void Awake()
    {
        _VCameraName = "CM vcam11";
        base.Awake();
        _originLenSize = CameraMgr.GetInstance().PlayerVirtualCamera.m_Lens.OrthographicSize;
        _path = _VCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path as CinemachineSmoothPath;
        _trackDolly = _VCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        EventMgr.GetInstance().AddLinstener<string>(_eventName, ResetCamera);
        _resetCamera = false;
    }

    /// <summary>
    /// 随着距离 放大镜头 和 缩小镜头
    /// </summary>
    /// <returns></returns>
    protected IEnumerator ZoomWithDistance()
    {

        while (true)
        {
            LensSettings lenSetting = _VCamera.m_Lens;
            float size = Mathf.Lerp(_originLenSize, TargetLenSize, _trackDolly.m_PathPosition);
            lenSetting.OrthographicSize = size;
            _VCamera.m_Lens = lenSetting;
            yield return null;
        }
    }


    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
        StartCoroutine(ZoomWithDistance());
    }

    protected override void DoWhenTriggerExit(Collider2D collision)
    {
        base.DoWhenTriggerExit(collision);
        StopCoroutine(ZoomWithDistance());
    }


    protected void ResetCamera(string info)
    {
        _resetCamera = true;
    }

    protected override void Update()
    {
        base.Update();
        if (_resetCamera)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("切换回来");
                CameraMgr.GetInstance().ChangeCamera(_VCamera, 2);
                //EventMgr.GetInstance().RemoveLinstener<string>(_eventName, ResetCamera);
                _resetCamera = false;
                StopCoroutine(ZoomWithDistance());
                StartCoroutine(ZoomWithDistance());
            }
        }
    }

}
