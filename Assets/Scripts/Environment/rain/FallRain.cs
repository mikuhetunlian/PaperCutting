using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRain : MonoBehaviour
{
  
    public GameObject RainBubble;
    /// �������еĵ�һ�� gameobject ��animator
    private Animator _subAnimator;
    private AnimatorStateInfo _stateInfor;
    private bool isActiveRainBubble;
    private void OnEnable()
    {
        Transform subTransform = transform.GetChild(0);
        _subAnimator = subTransform.GetComponent<Animator>();
    }


    private void Update()
    {
        WaitAndActiveRainBubble();
    }

    /// <summary>
    /// ������� ���ϲ鿴_subAnimator ��ǰ�Ķ���ʱ�� �������������� �� ���� rainbubble
    /// </summary>
    public void WaitAndActiveRainBubble()
    {
        _stateInfor = _subAnimator.GetCurrentAnimatorStateInfo(0);
        if (_stateInfor.IsName("animation"))
        {
            float time = _stateInfor.normalizedTime - Mathf.Floor(_stateInfor.normalizedTime);
            if (time > 0.95f && !isActiveRainBubble)
            {
                isActiveRainBubble = true;
                Invoke("ActiveRainBubble",0.5f);
                Debug.Log("�������");
            }
        }
    }

    /// <summary>
    /// ���� rainBubble
    /// </summary>
    public void ActiveRainBubble()
    {
        RainBubble.SetActive(true);
    }

}
