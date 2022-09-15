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
