using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Seed : MonoBehaviour,IResetAble
{
    public int ResponseCount;
    //销毁自己前要激活的对象
    public List<GameObject> ActiveList;
    //销毁自己前要失活的对象
    public List<GameObject> DeActiveList;
    protected Animator _animator;

    //下面两个参数需要参加到Reset中
    public bool _canResponse;
    public int _responseCount = 0;

    void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Response();
    }

    private void OnEnable()
    {
        Reset();
    }

    /// <summary>
    /// 种子响应player输入
    /// </summary>
    protected void Response()
    {
        AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        if (InputManager.GetInstance().ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonDown
            && clipInfo[0].clip.name.Equals("idle"))
        {
            _animator.SetBool("shake", true);
            _responseCount++;
        }
    }


    /// <summary>
    /// shanke动画事件
    /// </summary>
    public void ShankEvent()
    {
        _animator.SetBool("shake", false);
        if (_responseCount == ResponseCount)
        {
            StartCoroutine(Active());
        }
    }


    protected IEnumerator Active()
    {
            yield return null;
            foreach (GameObject obj in ActiveList)
            {
                obj.SetActive(true);
            }
            EventMgr.GetInstance().EventTrigger<string>("DestoryControlPanel", "DestoryControlPanel");
            //失活自己
            this.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _canResponse = true;
        _responseCount = 0;
        Debug.Log("reset seed");
    }
}
