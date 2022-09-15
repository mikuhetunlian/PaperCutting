using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


//�趨��ʼ���泯��
public enum FacingDirections { Left, Right }

/// <summary>
/// �������������������gameplay�ĵط�����ţ�
/// </summary>
public class Player : MonoBehaviour
{

   
    //��������泯�򣬻���ĳ����������泯����Ҫ�ı�
    public enum SpawnFacingDirections { Left, Right }
    public FacingDirections CurrentFaceingDir = FacingDirections.Right;
    public FacingDirections InitialFacingDir = FacingDirections.Right;
    public SpawnFacingDirections SpawnDir = SpawnFacingDirections.Right;

    //����player��״̬��������������ѣ�εȣ� �� player���ƶ�״̬
    public PlayerStates State;
    public StateMachine<PlayerStates.MovementStates> Movement;
    public StateMachine<PlayerStates.PlayerConditions> Condition;
    public Animator _animator;
    public PlayerAblity[] _playerAbilities;
    public List<string> _animatorParameterList { get; set;}

    public InputManager LinkedInputManager { get; protected set; }

    protected virtual void Awake()
    {
        Initialization();
        GetComponents();
    }

    //��ʼ��
    public void Initialization()
    {
        //��ʼ������״̬��
        Movement = new StateMachine<PlayerStates.MovementStates>(this.gameObject, false);
        Condition = new StateMachine<PlayerStates.PlayerConditions>(this.gameObject, false);

    }

    public void GetComponents()
    {
        GetAnimator();
        SetInputManager();
        _playerAbilities = GetComponents<PlayerAblity>();

    }



    public virtual void SetInputManager()
    {

        LinkedInputManager = InputManager.GetInstance();
        //UpDateInputManagerInAbilities();
    }

    /// <summary>
    /// ���� InputManager �����е� abilities��
    /// </summary>
    public virtual void UpDateInputManagerInAbilities()
    {
        if (_playerAbilities == null)
        {
            return;
        }
        for (int i = 0; i < _playerAbilities.Length; i++)
        {
            _playerAbilities[i].SetInputManager(LinkedInputManager);
        }
    }


    public virtual void GetAnimator()
    {
        _animator = GetComponent<Animator>();
        //����ҵ���aniamtor,�ͳ�ʼ��animator��Ĳ�����_animatorParametrs��
        if (_animator!=null)
        {
            InitializeAnimatorParameters();
        }
    }




    protected virtual void InitializeAnimatorParameters()
    {
        if (_animator == null)
        {
            return;
        }

        _animatorParameterList = new List<string>();

        AnimatorHelper.AddAnimatorParamaterIfExists(_animator, "isWalk", AnimatorControllerParameterType.Bool, _animatorParameterList);
    }




    // Update is called once per frame
    void Update()
    {
 
    }
}
