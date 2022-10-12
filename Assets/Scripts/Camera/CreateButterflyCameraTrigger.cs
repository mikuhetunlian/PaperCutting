using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class CreateButterflyCameraTrigger : CameraTriggerBase
{
    ///Ҫ���õ�NoiseProfile���ĸ�
    public NoiseSettings m_NoiseProfile;
    ///ҡ�ε����
    public float Amplitude;
    ///ҡ�ε�Ƶ��
    public float Frequency;
    [Tooltip("��һ�ηŴ󵽵�OrthographicSize")]
    public float OrthographicSize1;
    [Tooltip("�ڶ�����С�ĳ���ʱ��")]
    public float zoomInTime2;
    [Tooltip("��������С����OrthographicSize ���� ��ʼsize�� offset")]
    public float Orthographic3SizeOffset;
    [Tooltip("�����δη����Ŵ�ĳ���ʱ��")]
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
    /// ������ͷҡ��
    /// </summary>
    /// <param name="shakeTime">ҡ�γ�����ʱ��</param>
    public void CameraShake(int shakeTime)
    {
        StartCoroutine(DoCameraShake(shakeTime));
    }

    /// <summary>
    /// ʵʩ ��ͷҡ�ε�Э��
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

        //��ԭ��ͷ
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
