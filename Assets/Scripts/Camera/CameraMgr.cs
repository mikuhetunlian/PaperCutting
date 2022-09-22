using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMgr : BaseManager<CameraMgr>
{


    public Vector3 DefaultOffset = new Vector2(0,7.6f);
    private bool set;
    private CinemachineBrain _brain;
    

    public Dictionary<Camera,float> subCameraDic = new Dictionary<Camera, float>();
    public CameraMgr()
    {
        _brain = Camera.main.gameObject.GetComponent<CinemachineBrain>();

        //��ȡmainCamera�µ���������Ӱ�������Ҽ�¼�������mainCamera��size��ֵ
        GameObject mainCameraObj = Camera.main.gameObject;
        float mainSize = Camera.main.orthographicSize;
        for (int i = 0; i < mainCameraObj.transform.childCount; i++)
        {
            Camera cam = mainCameraObj.transform.GetChild(i).gameObject.GetComponent<Camera>();
            subCameraDic.Add(cam,mainSize - cam.orthographicSize);
        }

    }

    /// <summary>
    /// ����cinemachine��Ĭ�ϻ��ʱ��
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
    /// ��ÿ��������ͷ���ֺ�main cinemachine����Ծ��룬��ʹ��ResetCameraSize �Ļ������Ծ����һֱ������ȥ��Ҳ���ǻ�����һЩ����
    /// </summary>
    public void SetCamerasSize()
    {
        MonoManager.GetInstance().StartCoroutine(DoSetCamerasSize());
    }
    /// <summary>
    /// ���뿪��blend��ʱ���ʱ�򣬾Ϳ��Թر� DoSetCamerasSizeЭ�̴Ӷ���ԼһЩ����
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
            yield return  new WaitForFixedUpdate();
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

    /// <summary>
    /// ���õ�ǰ��ͷ�� offset Ϊ Ĭ�Ͼ�ͷ
    /// </summary>
    public void ResetCurrnteCameraOffset()
    {
        CinemachineVirtualCamera currentCamera = _brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        CinemachineFramingTransposer transporser = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        MonoManager.GetInstance().StartCoroutine(DoSetCurrentCameraOffset(transporser, DefaultOffset));

    }

    /// <summary>
    /// ���õ�ǰ��ͷ�� offsetX
    /// </summary>
    /// <param name="y"></param>
    public void SetCurrentCameraOffsetX(float x)
    {
        CinemachineVirtualCamera currentCamera = _brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        CinemachineFramingTransposer transporser = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        MonoManager.GetInstance().StartCoroutine(DoSetCurrentCameraOffset(transporser, new Vector2(x, transporser.m_TrackedObjectOffset.y)));
    }

    /// <summary>
    /// ���õ�ǰ��ͷ�� offsetY
    /// </summary>
    /// <param name="y"></param>
    public void SetCurrentCameraOffsetY( float y)
    {
        CinemachineVirtualCamera currentCamera = _brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        CinemachineFramingTransposer transporser = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        
        MonoManager.GetInstance().StartCoroutine(DoSetCurrentCameraOffset(transporser, new Vector2(transporser.m_TrackedObjectOffset.x, y)));
    }


    IEnumerator DoSetCurrentCameraOffset(CinemachineFramingTransposer transporser,Vector2 offset)
    {
        float t = 0;
        Vector3 originPoint = transporser.m_TrackedObjectOffset;
        while (t<=1)
        {
            transporser.m_TrackedObjectOffset = Vector3.Lerp(originPoint, offset, t);
            t += 0.05f;
            yield return null;
        }
        Debug.Log("�ﵽ���");
    }

}
