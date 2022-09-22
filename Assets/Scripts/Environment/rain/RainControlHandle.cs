using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainControlHandle : MonoBehaviour
{
    public GameObject RainStop;
    protected Animator _animator;
    private bool canBeControl;
    private bool isControlActive;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }



    private void Update()
    {
        if (canBeControl && !isControlActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                RainStop.GetComponent<rainStopTest>().StopRain();
                _animator.SetBool("on", true);
                isControlActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            canBeControl = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canBeControl = false;
        }
    }

}
