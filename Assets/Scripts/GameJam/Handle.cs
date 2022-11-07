using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    //�ı�handle��ʱ��
    public float ChangeTime;
    //��ת����Ŀ��Ƕ�
    public float TargetAngle;
    //handle��ԭ��ȥ��ʱ��
    public float ResetTime;
    public bool _canHandle;
    public bool detetcedPlayer;

    [Header("Music")]
    public AudioClip _handelFx;
    protected AudioSource _audioSource;

    protected Transform _transform;


    protected virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _canHandle = true;
        _transform = this.transform.GetChild(0);
    }

    private void Update()
    {
        if (detetcedPlayer &&
            InputManager.GetInstance().ControlButton.State.CurrentState == InputHelper.ButtonState.ButtonDown &&
            _canHandle)
        {
            _canHandle = false;
            StartCoroutine(PutHandle());
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            detetcedPlayer = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            detetcedPlayer = false;
        }
    }


    //��Handle
    protected virtual IEnumerator PutHandle()
    {
        _audioSource.clip = _handelFx;
        _audioSource.volume = 0.5f;
        _audioSource.Play();
        float t = 0;
        while (t <= ChangeTime)
        {
            float angle = Mathf.Lerp(0, TargetAngle, t / ChangeTime);
            _transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        DoWhenPutHandle();
        StartCoroutine(ResetHandle());
    }

    protected virtual void DoWhenPutHandle()
    {
        GiveWater();
    }


    //��ԭHandle
    protected IEnumerator ResetHandle()
    {
        float t = 0;
        while (t <= ResetTime)
        {
            float angle = Mathf.Lerp(TargetAngle, 0, t / ResetTime);
            _transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        _canHandle = true;
    }


    /// <summary>
    /// �����¼������ѱ��ڹ�ȥ��ʱ�򣬻�����ˮ
    /// </summary>
    public void GiveWater()
    {
        EventMgr.GetInstance().EventTrigger<float>("GiveWater", ResetTime);
    }
}
