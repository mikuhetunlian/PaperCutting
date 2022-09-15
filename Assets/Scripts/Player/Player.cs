using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


//设定初始的面朝向
public enum FacingDirections { Left, Right }

/// <summary>
/// 这个类是用来定义所有gameplay的地方（大概）
/// </summary>
public class Player : MonoBehaviour
{

   
    //重生后的面朝向，或许某次重生后的面朝向需要改变
    public enum SpawnFacingDirections { Left, Right }
    public FacingDirections CurrentFaceingDir = FacingDirections.Right;
    public FacingDirections InitialFacingDir = FacingDirections.Right;
    public SpawnFacingDirections SpawnDir = SpawnFacingDirections.Right;

    //包括player的状态（正常，冰冻，眩晕等） 和 player的移动状态
    public PlayerStates PlayerState;
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

    //初始化
    public void Initialization()
    {
        //初始化两个状态机
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
    /// 更新 InputManager 到所有的 abilities中
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
        //如果找到了aniamtor,就初始化animator里的参数到_animatorParametrs中
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
