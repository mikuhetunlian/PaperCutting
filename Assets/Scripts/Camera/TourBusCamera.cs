using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TourBusCamera : MonoBehaviour
{
    protected CinemachineVirtualCamera VCamera;

    void Start()
    {
        VCamera = GetComponent<CinemachineVirtualCamera>();
        EventMgr.GetInstance().AddLinstener<float>("Paper-cutConstructionWideAngleLens", ChangeToTourBusCamera);
    }


    /// <summary>
    /// �Զ��ν�ת�� 
    /// </summary>
    /// <param name="blendTime"></param>
    public void ChangeToTourBusCamera(float blendTime)
    {
        CameraMgr.GetInstance().SetDefaultBlednTime(blendTime);
        VCamera.enabled = true;

    }



}
