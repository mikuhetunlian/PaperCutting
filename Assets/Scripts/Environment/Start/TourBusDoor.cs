using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourBusDoor : MonoBehaviour
{
    protected Animator _animator;
    protected BoxCollider2D _collider;



    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = false;
    }


    /// <summary>
    /// 门往上升
    /// </summary>
    public void CloseDoor()
    {
        _animator.SetBool("up", true);
        _animator.SetBool("down", false);
        _collider.enabled = true;
    }

  
    /// <summary>
    /// 门往下降
    /// </summary>
    public void OpenDoor()
    {
        _animator.SetBool("down", true);
        _animator.SetBool("up", false);
        _collider.enabled = false;
    }

    

}
