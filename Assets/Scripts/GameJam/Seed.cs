using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Seed : MonoBehaviour,IResetAble
{
    public int ResponseCount;
    //�����Լ�ǰҪ����Ķ���
    public List<GameObject> ActiveList;
    //�����Լ�ǰҪʧ��Ķ���
    public List<GameObject> DeActiveList;
    protected Animator _animator;

    //��������������Ҫ�μӵ�Reset��
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
    /// ������Ӧplayer����
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
    /// shanke�����¼�
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
            //ʧ���Լ�
            this.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _canResponse = true;
        _responseCount = 0;
        Debug.Log("reset seed");
    }
}
