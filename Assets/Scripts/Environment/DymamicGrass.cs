using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DymamicGrass : MonoBehaviour
{
    /// <summary>
    /// �����Ƿ��򲥷ţ����ڷ�תͼ�ε�ʱ�򶯻��ܹ����շ�λ�Ĳ�ͬ��ȷ����
    /// </summary>
    public bool isAnimationRevers;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void ResetAnimatorParameter()
    {
        _animator.SetBool("isDown", false);
        _animator.SetBool("isLeft", false);
        _animator.SetBool("isRight", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {

            if (collision.gameObject.transform.position.y > this.transform.position.y)
            {
                _animator.SetBool("isDown",true);
            }
            else if (collision.gameObject.transform.position.x < this.transform.position.x)
            {
                if (isAnimationRevers)
                {
                    _animator.SetBool("isLeft", true);
                }
                else
                {
                    _animator.SetBool("isRight", true);
                }
            }
            else if (collision.gameObject.transform.position.x > this.transform.position.x)
            {
                if (isAnimationRevers)
                {
                    _animator.SetBool("isRight", true);
                }
                else
                {
                    _animator.SetBool("isLeft", true);
                }
               
            }
            Invoke("ResetAnimatorParameter", 0.5f);
        }
    }


}
