using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr:BaseManager<SceneMgr>
{
    /// <summary>
    /// 同步加载场景
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <param name="action">加载完成后的callback</param>
    public void LoadScene(string sceneName, UnityAction action = null)
    {
        SceneManager.LoadScene(sceneName);

        if (action != null)
        {
            action();
        }
    }


    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <param name="action">加载完成后的callback</param>
    public void LoadSceneAsync(string sceneName, UnityAction action = null)
    {
        MonoManager.GetInstance().StartCoroutine(DoLoadSceneAsync(sceneName, action));
    }


    //真正执行异步加载场景的协程函数
    private IEnumerator DoLoadSceneAsync(string sceneName, UnityAction action)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        while (!ao.isDone)
        {
            EventMgr.GetInstance().EventTrigger("loadsecneIng", ao.progress);
            yield return ao.progress;
        }
        if (action != null)
        {
            action();
        }
       
    }



}
