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

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
