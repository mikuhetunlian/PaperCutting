using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leafBlood : MonoBehaviour
{
    private Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 销毁自己 但其实可以用动画事件来做。。？
    /// </summary>
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
