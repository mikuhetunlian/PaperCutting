using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ResetGameByChangeScence : MonoBehaviour
{
    public PlayableDirector fianalTimeline;

    private void OnEnable()
    {
        fianalTimeline.Stop();
        SceneMgr.GetInstance().LoadScene("GameJamScene2");
    }
}
