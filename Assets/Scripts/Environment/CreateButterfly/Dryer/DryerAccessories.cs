using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerAccessories : MonoBehaviour
{

    public GameObject effect;
    /// ��Чչ����ʱ��Ҫת�Ƶ��������
    public CinemachineVirtualCamera GreatSizeCamera;
    ///�ƶ���GreatSizeCamera��Ҫ�Ĺ���ʱ��
    public float BlendTime;
    protected bool _canBeTouch;
    protected Jump _jump;

    private void Awake()
    {
        _canBeTouch = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && _canBeTouch)
        {
            Touch touch = collision.GetComponent<Touch>();
            touch.SetTouchIn();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && _canBeTouch)
        {
            if (InputManager.GetInstance().ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonDown)
            {
                DoWhenPlayerTouch(other);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && _canBeTouch)
        {
            Touch touch = collision.GetComponent<Touch>();
            touch.SetTouchOut();
        }
    }



    /// <summary>
    /// �� player �������Լ�ʱ�� 
    /// </summary>
    /// <param name="collision"></param>
    protected void DoWhenPlayerTouch(Collider2D collision)
    {
        //������Ч
        effect.SetActive(true);
        //��ӹ��ܣ��̶�����ҵ����Ͻ�
        KeepAtPoint keep = this.gameObject.AddComponent<KeepAtPoint>();
        keep.Target = collision.gameObject;
        keep.SetOffset(new Vector2(2, 2));
        //����player��touch Animator����
        Touch touch = collision.GetComponent<Touch>();
        touch.SetTouchOut();
        //�л������
        CameraMgr.GetInstance().ChangeCamera(GreatSizeCamera, BlendTime);
        //�����Ծ�߶�����
        _jump = collision.gameObject.GetComponent<Jump>();
        _jump.JumpHeight = 18;

        _canBeTouch = false;

        Invoke("EventTriggerRecover", BlendTime);
    }

    protected void EventTriggerRecover()
    {
        EventMgr.GetInstance().EventTrigger<string>("ChangeToGreateCamera", "ChangeToGreateCamera");
    }

    private void OnDestroy()
    {
        _jump.JumpHeight = 8;
    }
}
