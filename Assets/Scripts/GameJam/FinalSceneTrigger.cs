using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Cinemachine;
public class FinalSceneTrigger : MonoBehaviour
{
    public PlayableDirector finalSceneTimeLine;
    public Image screen;
    public float delayTime;
    public float fadeTime;
    public CinemachineVirtualCamera startCamera;

    //是否触发了最终场景
    protected bool isTriggerFinalScene;


    private void Start()
    {
        finalSceneTimeLine.stopped += (timeline) =>
        {
            StartCoroutine(FinalSceneTimelineStoppedCallback());
        };
    }

    /// <summary>
    /// 最终场景TimeLine结束的回调函数
    /// </summary>
    /// <returns></returns>
    protected IEnumerator FinalSceneTimelineStoppedCallback()
    {
        Debug.Log("结束回调");
        CameraMgr.GetInstance().SetDefaultBlendType(CinemachineBlendDefinition.Style.Cut);
        startCamera.enabled = false;
        startCamera.enabled = true;
        CinemachineTrackedDolly dolly = startCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_AutoDolly.m_Enabled = false;
        dolly.m_PathPosition = 0;
        InputManager.GetInstance().InputDetectionActive = false;
        yield return new WaitForSeconds(delayTime);
        float t = 0;
        Color color = screen.color;

        AudioSource bkMusic = MusicMgr.GetInstance().bkMusic;
        float originVolume = bkMusic.volume;
        while (t <= fadeTime)
        {
            //结束的时候渐变背景音乐
            bkMusic.volume = Mathf.Lerp(originVolume, 0, t / fadeTime);
            //结束的时候渐变画面
            float a = Mathf.Lerp(0, 1, t / fadeTime);
            screen.color = new Color(color.r, color.g, color.b, a);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        screen.color = new Color(color.r, color.g, color.b, 1);
        yield return null;
        UIMgr.GetInstance().Clear();
        SceneMgr.GetInstance().LoadScene("GameJamScene2");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player") && !isTriggerFinalScene)
        {
            //关掉所有输入
            InputManager.GetInstance().InputDetectionActive = false;
            //播放最终场景的TimeLine
            finalSceneTimeLine.Play();
            Debug.Log(other.transform.position);
            isTriggerFinalScene = true;
            other.gameObject.SetActive(false);
        }
    }
}
