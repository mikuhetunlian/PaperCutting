using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dryer : MonoBehaviour
{
    public PaperButterfly.WingDirection direction;
    public GameObject _paperButterflyObj;
    public float StartFlowDelay;
    protected PaperButterfly _paperButterfly;
    protected Animator _animator;
    protected string _paperButterflyName = "PaperButterfly_Origin";
    public ParticleSystem _particleEffect;
    ///�Ƿ�������������
    protected bool isOn;
    ///�Ƿ����д��������
    protected bool withDryerAccessories;

    void Start()
    {
        Initialization();
    }

    protected void Initialization()
    {
        _animator = GetComponent<Animator>();
        _particleEffect = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        _paperButterflyObj = GameObject.Find(_paperButterflyName);
        if (_paperButterflyObj != null)
        {
            _paperButterfly = _paperButterflyObj.GetComponent<PaperButterfly>();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("DryerAccessories") && !withDryerAccessories)
        {
            StartCoroutine(DoWhenDryerAccessoriesIn(collision));
        }
    }


    /// <summary>
    /// ������������������
    /// </summary>
    /// <param name="collison"></param>
    protected IEnumerator DoWhenDryerAccessoriesIn(Collider2D collison)
    {
        _animator.SetBool("withAccessories", true);
        withDryerAccessories = true;
        Destroy(collison.gameObject);

        yield return new WaitForSeconds(StartFlowDelay);

        _animator.SetBool("flow", true);
    }

    /// <summary>
    /// �����¼�
    /// </summary>
    public void PlayEffect()
    {
        if (!isOn)
        {
            _particleEffect.Play();
            //���粢�ҷ�ת��Ӧ�ߵĳ��

            isOn = true;
            if (_paperButterfly != null)
            {
                _paperButterfly.FlipWing(direction);
                Debug.Log("��������" + direction.ToString());
            }

            float delayTime = _particleEffect.main.duration;
            Invoke("SetAnimatorStateToIdle2", delayTime);
        }
      
    }

    /// <summary>
    /// �����ת��Idle״̬
    /// </summary>
    protected void SetAnimatorStateToIdle2()
    {
        _animator.SetBool("idle2", true);
    }

}
