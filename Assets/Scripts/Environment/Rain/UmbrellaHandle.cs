using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaHandle : MonoBehaviour
{
    ///��ɡ������
    public GameObject Umbrella;
    ///����handle���ӳټ�����ɡ��ʱ��
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
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Invoke("ActiveUmbrella", DeyalyTime);
                _animator.SetBool("on", true);
                isControlActive = true;
            }
        }
    }

    public void ActiveUmbrella()
    {
        Umbrella.GetComponent<PathMovement>().CanMove = true;
    }

    /// <summary>
    /// ����handle״̬
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
