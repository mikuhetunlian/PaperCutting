using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr:BaseManager<SceneMgr>
{
    /// <summary>
    /// ͬ�����س���
    /// </summary>
    /// <param name="sceneName">������</param>
    /// <param name="action">������ɺ��callback</param>
    public void LoadScene(string sceneName, UnityAction action = null)
    {
        SceneManager.LoadScene(sceneName);

        if (action != null)
        {
            action();
        }
    }


    /// <summary>
    /// �첽���س���
    /// </summary>
    /// <param name="sceneName">������</param>
    /// <param name="action">������ɺ��callback</param>
    public void LoadSceneAsync(string sceneName, UnityAction action = null)
    {
        MonoManager.GetInstance().StartCoroutine(DoLoadSceneAsync(sceneName, action));
    }


    //����ִ���첽���س�����Э�̺���
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
