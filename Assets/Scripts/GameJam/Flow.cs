using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    public bool isFlow;
    //开始吹风的延迟时间
    public float startDelayTime;
    //吹风的持续时间
    public float durationTime;
    //吹风的间隔时间
    public float deltaTime;
    //向左吹的风力 也就是player向左移动的水平速度
    public float flowerForce;
    public List<GameObject> Flows;

    [Header("Music")]
    public AudioClip flowFx;
    protected AudioSource _audioSource;

    protected GameObject _player;
    protected HorizontalMove _horizontalMove;
    protected PlayerController _playerController;
    protected bool _detect;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        EventMgr.GetInstance().AddLinstener<string>("DestoryFlow", DetoryFlow);
        Invoke("FlowMethod", startDelayTime);
    }

    protected void FlowMethod()
    {
        StartCoroutine(FlowRoutine());
    }

    /// <summary>
    /// 吹风的过程
    /// </summary>
    /// <returns></returns>
    protected IEnumerator FlowRoutine()
    {
        float t = 0;
        while (true)
        {
            t = 0;
            //吹风
            _audioSource.clip = flowFx;
            _audioSource.volume = 0.2f;
            _audioSource.Play();
            while (t <= durationTime)
            {
                _detect = true;
                if (_player != null)
                {
                    if (_playerController == null)
                    {
                        _playerController = _player.GetComponent<PlayerController>();
                    }
                    if (_horizontalMove == null)
                    {
                        _horizontalMove = _player.GetComponent<HorizontalMove>();
                    }

                    //关闭Input
                    InputManager.GetInstance().InputDetectionActive = false;
                    //失活水平移动
                    _horizontalMove.AbilityPermitted = false;

                    if (!_playerController.State.isCollidingLeft)
                    {
                        //向左移动
                        _playerController.SetHorizontalForce(-flowerForce);
                        _playerController.SetVerticalForce(0);
                    }
                    else
                    {
                        _playerController.SetHorizontalForce(0);
                    }
                    Debug.Log("施加向左速度");
                    //取消重力
                    _playerController.GravityActive(false);
                }
                foreach (GameObject obj in Flows)
                {
                    obj.SetActive(true);
                }
                isFlow = true;
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            t = 0;


            //不吹风
            while (t <= deltaTime)
            {

                _detect = false;
                isFlow = false;
                //在这表明曾经被风吹过
                if (_player != null)
                {
                    //打开输入开关
                    InputManager.GetInstance().InputDetectionActive = true;
                    //激活活水平移动
                    _horizontalMove.AbilityPermitted = true;
                    //水平速度为0
                    _playerController.SetHorizontalForce(0);
                    //重新获得重力
                    _playerController.GravityActive(true);
                }

                foreach (GameObject obj in Flows)
                {
                    obj.SetActive(false);
                }

                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            _player = null;
        }
      
    }


    /// <summary>
    /// 玩家通关后就停止吹风
    /// </summary>
    /// <param name="info"></param>
    public void DetoryFlow(string info)
    {
        StopAllCoroutines();
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && _detect)
        {
            _player = collision.gameObject;
        }
    }

}
