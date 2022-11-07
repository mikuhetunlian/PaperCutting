using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class StartCamera : MonoBehaviour,IResetAble
{

    public CinemachineVirtualCamera startCamera;

    public void Reset()
    {
        CameraMgr.GetInstance().SetDefaultBlendType(CinemachineBlendDefinition.Style.Cut);
        startCamera.enabled = false;
        startCamera.enabled = true;
        CinemachineTrackedDolly dolly = startCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_AutoDolly.m_Enabled = false;
        dolly.m_PathPosition = 0;
    }

   
}
