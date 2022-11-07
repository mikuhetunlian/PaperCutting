using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEditor.Animations;

public class ScissorFly : MonoBehaviour
{


    public PathCreator path;
    [Tooltip("�ƶ��ٶ�")]
    public float MoveSpeed;
    [Tooltip("�����ٶ�")]
    public float AnimationSpeed = 1;
    [Tooltip("�ﵽ�յ��ĺ����ƶ�ģʽ")]
    public EndOfPathInstruction MovementMode = EndOfPathInstruction.Reverse;
    [Tooltip("��תУ��")]
    public Vector3 AdjustVector = new Vector3(0,90,-90);

    protected Animator _animator;
    protected AnimatorController _animatorController;
    protected float _travelled;
    protected string _aniamtionName;


    private void Start()
    {
        Initilization();
    }


    protected void Initilization()
    {
        _animator = GetComponent<Animator>();
        AnimatorClipInfo clipinfo = _animator.GetCurrentAnimatorClipInfo(0)[0];
        _animatorController = _animator.runtimeAnimatorController as AnimatorController;
        _aniamtionName = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        SetAnimationSpeed(0, _aniamtionName, AnimationSpeed);
    }

    void Update()
    {
        SetAnimationSpeed(0, _aniamtionName, AnimationSpeed);
         _travelled += MoveSpeed * Time.deltaTime;
        transform.position = path.path.GetPointAtDistance(_travelled, MovementMode);
        transform.rotation = path.path.GetRotationAtDistance(_travelled, MovementMode);
        transform.rotation *= Quaternion.AngleAxis(AdjustVector.x, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(AdjustVector.y, Vector3.left);
        transform.rotation *= Quaternion.AngleAxis(AdjustVector.z, Vector3.forward);
        
    }


    /// <summary>
    /// �ı�animator��ĳ���������ٶ�
    /// </summary>
    /// <param name="layer">�㼶</param>
    /// <param name="stateName">����״̬��</param>
    /// <param name="speed">Ҫ�ĵ����ٶ�</param>
    protected void SetAnimationSpeed(int layer,string stateName,float speed)
    {
        for (int i = 0; i < _animatorController.layers[layer].stateMachine.states.Length; i++)
        {
            if (_animatorController.layers[layer].stateMachine.states[i].state.name == stateName)
            {
                _animatorController.layers[layer].stateMachine.states[i].state.speed = speed;
            }
        }
    }

}
