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
    /// ����Ҫ��base()֮ǰ ��д _VCameraName 
    /// �������Ҫ��Ҫ��awake����д _inBlendTime ��  _exitBlednTime��Ĭ��ֵ�ֱ��� 2 �� 2
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
    /// Ĭ���뿪��ʱ�򲻻�����������ǵ�size��ƫ��
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void DoWhenTriggerExit(Collider2D collision)
    {
        _VCamera.enabled = false;
        CameraMgr.GetInstance().SetDefaultBlednTime(_exitBlednTime);

    }


   




}
