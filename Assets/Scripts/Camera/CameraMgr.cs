using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMgr : BaseManager<CameraMgr>
{

    private bool set;
    private CinemachineBrain _brain;

    public Dictionary<Camera,float> subCameraDic = new Dictionary<Camera, float>();
    public CameraMgr()
    {
        _brain = Camera.main.gameObject.GetComponent<CinemachineBrain>();

        //获取mainCamera下的所有子摄影机，并且记录下相对于mainCamera的size差值
        GameObject mainCameraObj = Camera.main.gameObject;
        float mainSize = Camera.main.orthographicSize;
        for (int i = 0; i < mainCameraObj.transform.childCount; i++)
        {
            Camera cam = mainCameraObj.transform.GetChild(i).gameObject.GetComponent<Camera>();
            subCameraDic.Add(cam,mainSize - cam.orthographicSize);
        }

    }

    /// <summary>
    /// 设置cinemachine的默认混合时间
    /// </summary>
    public void SetDefaultBlednTime(float blendTime)
    {
        if (_brain == null)
        {
            return;
        }

        _brain.m_DefaultBlend.m_Time = blendTime;
    }

    /// <summary>
    /// 让每个子摄像头保持和main cinemachine的相对距离，不使用ResetCameraSize 的话这个相对距离会一直保持下去，也就是会消耗一些性能
    /// </summary>
    public void SetCamerasSize()
    {
        MonoManager.GetInstance().StartCoroutine(DoSetCamerasSize());
    }
    /// <summary>
    /// 当离开了blend的时间的时候，就可以关闭 DoSetCamerasSize协程从而节约一些性能
    /// </summary>
    public void ResetCameraSize()
    {
        MonoManager.GetInstance().StartCoroutine(DoResetCameraSize());
    }

    private IEnumerator DoSetCamerasSize()
    {
        set = true;
        while (set)
        {
            Camera maincamera = Camera.main;
            foreach (Camera camera in subCameraDic.Keys)
            {
                camera.orthographicSize = maincamera.orthographicSize + subCameraDic[camera];
            }
            yield return new WaitForFixedUpdate();
        }

    }

    private IEnumerator DoResetCameraSize()
    {
        set = false;
        yield return new WaitForSeconds(2);
        Camera maincamera = Camera.main;
        foreach (Camera camera in subCameraDic.Keys)
        {
            camera.orthographicSize = maincamera.orthographicSize + subCameraDic[camera];
        }
    }



}
