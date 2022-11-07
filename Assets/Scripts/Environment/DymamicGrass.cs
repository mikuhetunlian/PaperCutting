using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DymamicGrass : MonoBehaviour
{
    [Tooltip("动画是否反向播放，用于翻转图形的时候动画能够按照方位的不同正确播放")]
    /// <summary>
    /// 动画是否反向播放，用于翻转图形的时候动画能够按照方位的不同正确播放
    /// </summary>
    public bool isAnimationRevers;

    protected Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }

 

  

    //重置动画参数
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

            //如果在左边
            if (collision.gameObject.transform.position.x < this.transform.position.x)
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
            //如果在右边
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
            //如果在上方
            else if (collision.gameObject.transform.position.y > this.transform.position.y)
            {
                _animator.SetBool("isDown", true);
            }
            Invoke("ResetAnimatorParameter", 0.5f);
        }
    }


}
