using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePlatform : MonoBehaviour
{
    public AudioClip _loopPlatformFx;
    protected BoxCollider2D _collider;
    protected GameObject _collionObj;
    protected AudioSource _audioSource;
    Vector2 leftOrign;
    Vector2 rightOrign;
    float rayNum;
    float rayLength;
    //������һ���Ӵ��������������
    protected GameObject _lastCollisonObj;
    //�Ƿ��Ѿ�������player
    protected bool haveResetPlayer;
    
    

    void Start()
    {
         _audioSource = GetComponent<AudioSource>();
         _collider = GetComponent<BoxCollider2D>();
         leftOrign = _collider.bounds.center - new Vector3(_collider.bounds.extents.x , 0, 0);
         rightOrign = _collider.bounds.center + new Vector3(_collider.bounds.extents.x , 0, 0);
         rayNum = 60;
         rayLength = _collider.bounds.extents.y + 0.1f;
         Physics2D.queriesStartInColliders = false;
         haveResetPlayer = false;
    }

    void Update()
    {
        DetectPlayer();
    }


    protected void DetectPlayer()
    {
        bool isDetected = false;
        for (int i = 0; i < rayNum; i++)
        {
            Vector2 orrgin = Vector2.Lerp(leftOrign, rightOrign, (float)i / (rayNum - 1));
            RaycastHit2D hitInfo = DebugHelper.RaycastAndDrawLine(orrgin, Vector2.up, rayLength, LayerMgr.PlayerLayerMask);
            if (hitInfo.collider != null)
            {
                isDetected = true;
                _collionObj = hitInfo.collider.gameObject;
                _lastCollisonObj = _collionObj;
            }
        }

        //�����⵽player��ǿ��λ�ƣ�������Ч���뿪����reset�����ٴ���
        if (isDetected && !haveResetPlayer)
        {
            //�ر�����
            InputManager.GetInstance().InputDetectionActive = false;
            HorizontalMove horizontalMove = _collionObj.GetComponent<HorizontalMove>();
            Jump jump = _collionObj.GetComponent<Jump>();
            PlayerController playerController = _collionObj.GetComponent<PlayerController>();

            _audioSource.clip = _loopPlatformFx;
            _audioSource.volume = 0.5f;
            _audioSource.Play();


            playerController.SetHorizontalForce(-14f);
            horizontalMove.AbilityPermitted = false;
            jump.AbilityPermitted = false;
            haveResetPlayer = true;
        }

        //��player��ƽ̨���뿪ʱ
        if (!isDetected && haveResetPlayer)
        {
            //������
            InputManager.GetInstance().InputDetectionActive = true;
            HorizontalMove horizontalMove = _lastCollisonObj.GetComponent<HorizontalMove>();
            Jump jump = _lastCollisonObj.GetComponent<Jump>();
            PlayerController playerController = _collionObj.GetComponent<PlayerController>();
            playerController.SetHorizontalForce(0);
            horizontalMove.AbilityPermitted = true;
            jump.AbilityPermitted = true;
            haveResetPlayer = false;
            _audioSource.Stop();
        }
       
    }
}
