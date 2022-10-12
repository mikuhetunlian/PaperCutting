using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainStopTest : MonoBehaviour
{

    //下雨环境声的物体
    public GameObject RainBk;
    //停止下雨的时间
    public float stopTime;
    private bool stopRain;
    //获得子物体的所有animator
    private Animator[] _animators;
    private RainRoot _rains;
    //是否停止下雨了
    private bool _isStop;


    void Start()
    {
        _animators = this.GetComponentsInChildren<Animator>();
        _rains = this.GetComponent<RainRoot>();
    }


    void Update()
    {
        if (stopRain && !_isStop)
        {
            _isStop = true;
            StopRainBk();
            for (int i = 0; i < _animators.Length; i++)
            {
                _animators[i].SetBool("stop", true);
                _rains.StopDetect();
            }

        }
    }

    public void StopRain()
    {
        stopRain = true;
    }

    /// <summary>
    /// 停止下雨的背景音乐
    /// </summary>
    public void StopRainBk()
    {
        StartCoroutine(DoStopRainBk(RainBk.GetComponent<AudioSource>(), stopTime));
    }

    /// <summary>
    /// 执行停止下雨的声音协程
    /// </summary>
    /// <param name="source"></param>
    /// <param name="stopTime"></param>
    /// <returns></returns>
    public IEnumerator DoStopRainBk(AudioSource source,float stopTime)
    {
        float volume = source.volume;
        float t = 0;

        while (t < stopTime)
        {
            source.volume = Mathf.Lerp(volume, 0, t / stopTime);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("减少完毕");
    }



}