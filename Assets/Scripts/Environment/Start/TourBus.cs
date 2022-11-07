using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class TourBus : MonoBehaviour
{
    ///四个轮子
    public List<AutoRotate> Wheels;
    ///车上的两扇门
    public List<TourBusDoor> Doors;
    ///阻挡前进的门
    public List<PathMovement> BlockDoors;

    [Tooltip("转移到 Paper-cutConstructionWideAngleLens 的偏移时间")]
    ///转移到 Paper-cutConstructionWideAngleLens 的偏移时间
    public float offsetTime;


    [Header("Camera")]
    public CinemachineVirtualCamera StartDownCamera;
    
    protected PathMovement _pathMovement;

    //起点到终点的时间
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
    /// 开门
    /// </summary>
    public void OpenDoor()
    {
        foreach (TourBusDoor door in Doors)
        {
            door.OpenDoor();
        }
    }

    /// <summary>
    /// 关门
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
    /// 滚动车轮
    /// </summary>
    public void RunWheel()
    {
        foreach (AutoRotate wheel in Wheels)
        {
            wheel.SetRotate(true);
        }
    }

    /// <summary>
    /// 停止车轮
    /// </summary>
    public void StopWheel()
    {
        foreach (AutoRotate wheel in Wheels)
        {
            wheel.SetRotate(false);
        }
    }

    /// <summary>
    /// 升起阻挡的门
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
            //失活跳跃
            _jump = collision.gameObject.GetComponent<Jump>();
            _jump.AbilityPermitted = false;

            StartCoroutine(OpenTourBus());
        }
    }


    /// <summary>
    /// 开车
    /// </summary>
    /// <returns></returns>
    protected IEnumerator OpenTourBus()
    {
        CloseDoor();
        yield return new WaitForSeconds(0.8f);
        //高速 TourBusCamera 切换镜头
        EventMgr.GetInstance().EventTrigger<float>("Paper-cutConstructionWideAngleLens", moveTime);
        //这里是 观看完 剪纸后 镜头缩放回去
        Invoke("OpenDoorAndChangeCamera", moveTime + offsetTime);
        RunWheel();
        _pathMovement.CanMove = true;

       
    }


    protected void OpenDoorAndChangeCamera()
    {
        StartCoroutine(OpenDoorAndChangeCameraCoroutine());
    }

    /// <summary>
    /// 开门 + 切换镜头 + 重新跳跃 + 停车 + 激活蝴蝶们
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
