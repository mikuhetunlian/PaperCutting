using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handle �Ļ���
/// </summary>
public class HandleBase : MonoBehaviour
{
    public enum HandleType
    {
        ///Handle ֻ�ܲ���һ��
        OnceOnly,
        ///Handle �ܷ�������
        Repeating
    }

    public enum HandleState
    {
        ///Handle ��Ч
        On,
        ///Handle ����Ч
        Off
    }

    public HandleType handleType = HandleType.Repeating;
    public HandleState handleState = HandleState.Off;
    public bool CanBeControl => _canBeControl;



    protected Animator _animator;
    protected bool _canBeControl = false;
    protected bool _onlyOnceCanBeControl = true;
    protected bool _isChanging;


    private void Start()
    {
        Initilization();
    }


    protected virtual void Initilization()
    {
        _animator = GetComponent<Animator>();
    }



    /// <summary>
    /// ��Handle��Ҫ�������� , spine�����¼�
    /// </summary>
    protected virtual void TurnOn()
    { 

    }

    /// <summary>
    /// �ر�Handle��Ҫ��������, spine�����¼�
    /// </summary>
    protected virtual void TurnOff()
    {
        
    }

    /// <summary>
    /// Hanlde��ת���� ,spine�����¼���base()������
    /// </summary>
    protected virtual void ChangeOver()
    {
        _isChanging = false;
    }


    protected virtual void Update()
    {

        if (_canBeControl &&
            InputManager.GetInstance().ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonDown)
        {

            switch(handleType)
            {
                case HandleType.OnceOnly:
                    //�����OnceOnly���͵Ļ���ֻ�ܴ���һ�Σ�����ͨ��  ���������ò���
                    if (_onlyOnceCanBeControl)
                    {
                        ChangeHandleState();
                        _onlyOnceCanBeControl = false;
                    }
                    break;

                case HandleType.Repeating:
                    ChangeHandleState();
                    break;

            }
        }
    }



    /// <summary>
    /// �ı�handle״̬
    /// </summary>
    protected virtual void ChangeHandleState()
    {
        //�������ת�Ĺ��̣�handle���ܱ��ٿ�
        if (_isChanging)
        {
            return;
        }

        if (handleState == HandleState.Off)
        {
            handleState = HandleState.On;
            TurnOnChangeAnimatorParameter();
        }
        else if (handleState == HandleState.On)
        {
            handleState = HandleState.Off;
            TurnOffChangeAnimatorParameter();
        }
        _isChanging = true;

    }

    /// <summary>
    /// TurnOn ʱ��Ҫ �ı��Animator����
    /// </summary>
    protected virtual void TurnOnChangeAnimatorParameter()
    {
        
    }

    /// <summary>
    /// TurnOff ʱ��Ҫ �ı��Animator����
    /// </summary>
    protected virtual void TurnOffChangeAnimatorParameter()
    {

    }



    /// <summary>
    /// ����Handle��״̬������ ��ʼ״̬����OnlyOnce�����¿����ٴα����ݵĿ���
    /// </summary>
    public virtual void ResetHandleState()
    {
        if (handleType == HandleType.OnceOnly)
        {
            _onlyOnceCanBeControl = true;
        }

        handleState = HandleState.Off;
    }


    /// <summary>
    /// player ����trigger��handle���Ա��ٿ�
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            _canBeControl = true;
            DoWhenPlayerIn();
        }
    }

    /// <summary>
    /// player ����trigger ��Ҫ��������������
    /// </summary>
    protected virtual void DoWhenPlayerIn()
    {
        
    }


    /// <summary>
    /// player �뿪trigger��handle���Ա��ٿ�
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void  OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            _canBeControl = false;
            DoWhenPlayerExit();
        }
    }

    /// <summary>
    /// player ����trigger ��Ҫ��������������
    /// </summary>
    protected virtual void DoWhenPlayerExit()
    {

    }
}
