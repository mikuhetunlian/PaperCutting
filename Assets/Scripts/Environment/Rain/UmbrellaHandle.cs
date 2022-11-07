using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaHandle : MonoBehaviour
{
    ///雨伞的引用
    public GameObject Umbrella;
    ///拉下handle后延迟激活雨伞的时间
    public float DeyalyTime;
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
            if (InputManager.GetInstance().ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonDown)
            {
                Invoke("ActiveUmbrella", DeyalyTime);
                _animator.SetBool("on", true);
                isControlActive = true;
            }
        }
    }

    /// <summary>
    /// 激活雨伞移动
    /// </summary>
    public void ActiveUmbrella()
    {
        Umbrella.GetComponent<PathMovement>().CanMove = true;
        Debug.Log("激活了伞");
    }

    /// <summary>
    /// 重置handle状态
    /// </summary>
    public void ResetHandleState()
    {
        canBeControl = true;
        isControlActive = false;
        _animator.SetBool("on", false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
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
