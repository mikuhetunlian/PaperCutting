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
    ///是否启动吹风了上
    protected bool isOn;
    ///是否配有吹风机附件
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
    /// 当触碰到到吹风机配件
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
    /// 动画事件
    /// </summary>
    public void PlayEffect()
    {
        if (!isOn)
        {
            _particleEffect.Play();
            //吹风并且翻转对应边的翅膀

            isOn = true;
            if (_paperButterfly != null)
            {
                _paperButterfly.FlipWing(direction);
                Debug.Log("吹出风了" + direction.ToString());
            }

            float delayTime = _particleEffect.main.duration;
            Invoke("SetAnimatorStateToIdle2", delayTime);
        }
      
    }

    /// <summary>
    /// 吹完风转到Idle状态
    /// </summary>
    protected void SetAnimatorStateToIdle2()
    {
        _animator.SetBool("idle2", true);
    }

}
