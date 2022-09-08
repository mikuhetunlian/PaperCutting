using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

/// <summary>
/// �������������������gameplay�ĵط�����ţ�
/// </summary>
public class Player : MonoBehaviour
{

    //�趨��ʼ���泯��
    public enum FacingDirections { Left, Right }
    //��������泯�򣬻���ĳ����������泯����Ҫ�ı�
    public enum SpawnFacingDirections { Left, Right }

    public FacingDirections InitialFacingDir = FacingDirections.Right;
    public SpawnFacingDirections SpawnDir = SpawnFacingDirections.Right;

    //����player��״̬��������������ѣ�εȣ� �� player���ƶ�״̬
    public PlayerStates PlayerState;
    public StateMachine<PlayerStates.MovementStates> Movement;
    public StateMachine<PlayerStates.PlayerConditions> Condition;
    public Animator _animator;
    public List<string> _animatorParameterList { get; set;}



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
