using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class CreateButterflyCameraTrigger : CameraTriggerBase
{
    ///要是用的NoiseProfile是哪个
    public NoiseSettings m_NoiseProfile;
    ///摇晃的振幅
    public float Amplitude;
    ///摇晃的频率
    public float Frequency;
    [Tooltip("第一次放大到的OrthographicSize")]
    public float OrthographicSize1;
    [Tooltip("第二次缩小的持续时间")]
    public float zoomInTime2;
    [Tooltip("第三次缩小到的OrthographicSize 距离 初始size的 offset")]
    public float Orthographic3SizeOffset;
    [Tooltip("第三次次反弹放大的持续时间")]
    public float zoomInTime3;
    protected CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
    protected LensSettings _lenSetting;



    protected override void Awake()
    {
        _VCameraName = "CMvcamButterflyAlive";
        base.Awake();
        EventMgr.GetInstance().AddLinstener<int>("CameraShake", CameraShake);
        _lenSetting = _VCamera.m_Lens;
    }

    protected override void DoWhenTriggerEnter(Collider2D collision)
    {
        base.DoWhenTriggerEnter(collision);
    }

    /// <summary>
    /// 启动镜头摇晃
    /// </summary>
    /// <param name="shakeTime">摇晃持续的时长</param>
    public void CameraShake(int shakeTime)
    {
        StartCoroutine(DoCameraShake(shakeTime));
    }

    /// <summary>
    /// 实施 镜头摇晃的协程
    /// </summary>
    /// <param name="shakeTime"></param>
    /// <returns></returns>
    protected IEnumerator DoCameraShake(int shakeTime)
    {

        CinemachineTrackedDolly trackedDolly = _VCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        trackedDolly.m_AutoDolly.m_Enabled = false;
        trackedDolly.m_PathPosition = 0.58f;
        float t = 0;

        _multiChannelPerlin = _VCamera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _multiChannelPerlin.m_NoiseProfile = m_NoiseProfile;

        _multiChannelPerlin.m_AmplitudeGain = Amplitude;
        _multiChannelPerlin.m_FrequencyGain = Frequency;


        LensSettings originLenSetting = _VCamera.m_Lens;
        LensSettings endLenSetting = originLenSetting;
        endLenSetting.OrthographicSize = OrthographicSize1;

        while (t < shakeTime)
        {
            _VCamera.m_Lens = LensSettings.Lerp(originLenSetting,endLenSetting,t / shakeTime);
            t += Time.deltaTime;
            yield return null;
        }
        _multiChannelPerlin.m_AmplitudeGain = 0;
        _multiChannelPerlin.m_FrequencyGain = 0;

        //复原镜头
        float zoomInTime = zoomInTime2;
        LensSettings zoomInLenSetting = originLenSetting;
        zoomInLenSetting.OrthographicSize += Orthographic3SizeOffset;
        t = 0;
        while (t < zoomInTime)
        {
            _VCamera.m_Lens = LensSettings.Lerp(endLenSetting, zoomInLenSetting, t / zoomInTime);
            t += Time.deltaTime;
            yield return null;
        }

        float zoomOutTime = zoomInTime3;
        t = 0;
        while (t < zoomOutTime)
        {
            _VCamera.m_Lens = LensSettings.Lerp(zoomInLenSetting, originLenSetting, t / zoomOutTime);
            t += Time.deltaTime;
            yield return null;
        }

        trackedDolly.m_AutoDolly.m_Enabled = true;
    }

}
