using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ability�Ļ���
/// </summary>
public class PlayerAblity :MonoBehaviour
{

    ///�����Ƿ�����ʹ��
    public bool AbilityPermitted = true;


    protected PlayerController _playerController;
    protected Player _player;
    protected Animator _animator;
    protected StateMachine<PlayerStates.MovementStates> _movement;
    protected StateMachine<PlayerStates.PlayerConditions> _condition;


    protected virtual void Start()
    {
        GetComponents();
        Initialization();
    }
    /// <summary>
    /// ability ��ʼ�����ݺ�����һЩ����
    /// </summary>
    public virtual void Initialization()
    {
        
    }
    /// <summary>
    /// ability �����Ҫ��õ����
    /// </summary>
    public virtual void GetComponents()
    {
        _playerController = GetComponent<PlayerController>();
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _movement = _player.Movement;
        _condition = _player.Condition;
        if (_animator != null)
        {
            InitializeAnimatorParameter();
        }
    }


    /// <summary>
    /// abilityʹ�õĿ���
    /// </summary>
    /// <param name="abilityPermitted"></param>
    public virtual void PermitAbility(bool abilityPermitted)
    {
        AbilityPermitted = abilityPermitted;

    }


    //ability�ĵ�һ��
    public virtual void EarlyProcessAblity()
    {
        
    }

    //ability�ĵڶ���
    public virtual void ProcessAbility()
    {
        
    }

    //abilitty�ĵ�����
    public virtual void LateProcessAbility()
    {

    }

    /// <summary>
    /// ��д�������������Լ�ability��Ҫ�����animator�Ĳ�����Player��_animatorParameterList��
    /// �� awake �� GetComponents �л�����������
    /// </summary>
    protected virtual void InitializeAnimatorParameter()
    {
        
    }

    /// <summary>
    /// ���animator�д�������������Ͱ�����ӵ�Player��_animatorParameterList��
    /// </summary>
    /// <param name="parameterName"></param>
    /// <param name="type"></param>
    protected virtual void RegisterAnimatorParameter(string parameterName, AnimatorControllerParameterType type)
    {
        if (_animator == null)
        {
            return;
        }
        if (_animator.HasParameterOfType(parameterName, type))
        {
            _player._animatorParameterList.Add(parameterName);
        }
    }

    /// <summary>
    /// abilitys ��д������� ���ı�player���ϵ�animator�Ĳ�����ÿһִ֡��һ�Σ�����ÿһ��lateUpdate�����
    /// </summary>
    public virtual void UpdateAnimator()
    {
        
    }



}
