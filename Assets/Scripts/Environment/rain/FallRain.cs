using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRain : MonoBehaviour
{
  
    public GameObject RainBubble;
    /// 子物体中的第一个 gameobject 的animator
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
    /// 被激活后 不断查看_subAnimator 当前的动画时间 如果将近播放完毕 就 激活 rainbubble
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
                Debug.Log("播放完毕");
            }
        }
    }

    /// <summary>
    /// 激活 rainBubble
    /// </summary>
    public void ActiveRainBubble()
    {
        RainBubble.SetActive(true);
    }

}
