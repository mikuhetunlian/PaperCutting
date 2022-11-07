using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class TourBus : MonoBehaviour
{
    ///�ĸ�����
    public List<AutoRotate> Wheels;
    ///���ϵ�������
    public List<TourBusDoor> Doors;
    ///�赲ǰ������
    public List<PathMovement> BlockDoors;

    [Tooltip("ת�Ƶ� Paper-cutConstructionWideAngleLens ��ƫ��ʱ��")]
    ///ת�Ƶ� Paper-cutConstructionWideAngleLens ��ƫ��ʱ��
    public float offsetTime;


    [Header("Camera")]
    public CinemachineVirtualCamera StartDownCamera;
    
    protected PathMovement _pathMovement;

    //��㵽�յ��ʱ��
    protected float moveTime;
    protected bool isCloseDoor;
    protected Jump _jump;


    private void Start()
    {
        _pathMovement = GetComponent<PathMovement>();
        moveTime = Mathf.Abs(_pathMovement.PathElements[0].PathElementPotion.x - _pathMovement.PathElements[1].PathElementPotion.x) / _pathMovement.MovementSpeed;
        moveTime += offsetTime;
    }



    /// <summary>
    /// ����
    /// </summary>
    public void OpenDoor()
    {
        foreach (TourBusDoor door in Doors)
        {
            door.OpenDoor();
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public void CloseDoor()
    {
        foreach (TourBusDoor door in Doors)
        {
            door.CloseDoor();
        }
        isCloseDoor = true;
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void RunWheel()
    {
        foreach (AutoRotate wheel in Wheels)
        {
            wheel.SetRotate(true);
        }
    }

    /// <summary>
    /// ֹͣ����
    /// </summary>
    public void StopWheel()
    {
        foreach (AutoRotate wheel in Wheels)
        {
            wheel.SetRotate(false);
        }
    }

    /// <summary>
    /// �����赲����
    /// </summary>
    public void OpenBlockDoor()
    {
        foreach (PathMovement door in BlockDoors)
        {
            door.CanMove = true;
            door.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !isCloseDoor)
        {
            //ʧ����Ծ
            _jump = collision.gameObject.GetComponent<Jump>();
            _jump.AbilityPermitted = false;

            StartCoroutine(OpenTourBus());
        }
    }


    /// <summary>
    /// ����
    /// </summary>
    /// <returns></returns>
    protected IEnumerator OpenTourBus()
    {
        CloseDoor();
        yield return new WaitForSeconds(0.8f);
        //���� TourBusCamera �л���ͷ
        EventMgr.GetInstance().EventTrigger<float>("Paper-cutConstructionWideAngleLens", moveTime);
        //������ �ۿ��� ��ֽ�� ��ͷ���Ż�ȥ
        Invoke("OpenDoorAndChangeCamera", moveTime + offsetTime);
        RunWheel();
        _pathMovement.CanMove = true;

       
    }


    protected void OpenDoorAndChangeCamera()
    {
        StartCoroutine(OpenDoorAndChangeCameraCoroutine());
    }

    /// <summary>
    /// ���� + �л���ͷ + ������Ծ + ͣ�� + ���������
    /// </summary>
    /// <returns></returns>
    protected IEnumerator OpenDoorAndChangeCameraCoroutine()
    {
        CameraMgr.GetInstance().SetDefaultBlednTime(2f);
        CameraMgr.GetInstance().ChangeCamera(StartDownCamera, 3);
        //CameraMgr.GetInstance().PlayerVirtualCamera.enabled = false;
        //CameraMgr.GetInstance().PlayerVirtualCamera.enabled = true;
        StopWheel();
        yield return new WaitForSeconds(1.8f);
        OpenDoor();
        OpenBlockDoor();
        _jump.AbilityPermitted = true;
        EventMgr.GetInstance().EventTrigger<float>("ActiveButterfly", 3f);
    }

}
