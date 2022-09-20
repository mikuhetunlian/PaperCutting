using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingeltonAutoManager<InputManager>
{
    [Header("Stauts")]
    /// InputManager ���ܿ���
    public bool InputDetectionActive = true;
    public string playerID = "Player1";

    //�Ƿ��������ƶ�
    public bool SmoothMovement;
    public List<InputHelper.IMButton> ButtonList;
    public Vector2 PrimaryMovement { get { return _primaryMovement; }}

    protected Vector2 _primaryMovement = Vector2.zero;
    protected string _axisHorizontal;
    protected string _axisVertical;



    public InputHelper.IMButton JumpButton { get; protected set; }

    protected void Start()
    {
        InitializaButtons();
        InitializeAixs();
    }

    /// <summary>
    /// ��ʼ�����������ӵ� buttonList ��
    /// </summary>
    protected void InitializaButtons()
    {
        ButtonList = new List<InputHelper.IMButton>();
        ButtonList.Add(JumpButton = new InputHelper.IMButton(playerID, "Jump", JumpButtonDown, JumpButtonPresswd, JumpButtonUp));
    }

    /// <summary>
    /// ��ʼ��������
    /// </summary>
    protected void InitializeAixs()
    {
        _axisHorizontal = playerID + "_Horizontal";
        _axisVertical = playerID + "_Vertical";
    }

    protected void LateUpdate()
    {
        ProcessButtonStates();
    }

    protected void Update()
    {
        SetMovement();
        SetButtons();
    }

    /// <summary>
    /// ÿһ֡���ˮƽ�����ֱ���ֵ ���� _primaryMovement
    /// </summary>
    public void SetMovement()
    {
        if (SmoothMovement)
        {
            _primaryMovement.x = Input.GetAxis(_axisHorizontal);
            _primaryMovement.y = Input.GetAxis(_axisVertical);
        }
        else
        {
            _primaryMovement.x = Input.GetAxisRaw(_axisHorizontal);
            _primaryMovement.y = Input.GetAxisRaw(_axisVertical);
        }

    }

    /// <summary>
    /// ÿһ֡������button��״̬
    /// </summary>
    public virtual void SetButtons()
    {
        foreach (InputHelper.IMButton button in ButtonList)
        {

            if (Input.GetButton(button.ButtonID))
            {
                button.TriggerButtonPressed();
            }
            if (Input.GetButtonDown(button.ButtonID))
            {
                button.TriggerButtonDown();
            }
            if (Input.GetButtonUp(button.ButtonID))
            {
                button.TriggerButtonUp();
            }
        }
    }

    /// <summary>
    /// ��lateUpdate��ִ�У��������� ������״̬
    /// </summary>
    public virtual void ProcessButtonStates()
    {
        foreach (InputHelper.IMButton button in ButtonList)
        {
            if (button.State.CurrentState == InputHelper.ButtonState.ButtonDown)
            {
                StartCoroutine(DelayButtonPress(button));
            }
            if (button.State.CurrentState == InputHelper.ButtonState.ButtonUp)
            {
                button.State.ChangeState(InputHelper.ButtonState.Off);
            }
        }
    }
    /// <summary>
    /// �����֡�� buttonDown ��һ֡��������Ϊ buttonPressed ��״̬
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    protected IEnumerator DelayButtonPress(InputHelper.IMButton button)
    {
        //����һ��cycle�� update �� lateUpdate֮��ִ��
        yield return null;
        button.State.ChangeState(InputHelper.ButtonState.ButtonPressed);
    }

    public void JumpButtonDown() { JumpButton.State.ChangeState(InputHelper.ButtonState.ButtonDown); }
    public void JumpButtonPresswd() { JumpButton.State.ChangeState(InputHelper.ButtonState.ButtonPressed); }
    public void JumpButtonUp() 
    {
        JumpButton.State.ChangeState(InputHelper.ButtonState.ButtonUp);
  
    }





}