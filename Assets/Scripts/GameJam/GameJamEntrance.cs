using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameJamEntrance : MonoBehaviour
{
    public Image screen;
    public float delayTime;
    public float fadeTime;

    private void Start()
    {
        UIMgr.GetInstance().Clear();
        StartCoroutine(FadeBlackToTrancy());
        //Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneMgr.GetInstance().LoadScene("GameJamUI");
        }
    }

    /// <summary>
    /// 从黑渐变到透明
    /// </summary>
    /// <returns></returns>
    protected IEnumerator FadeBlackToTrancy()
    {
        InputManager.GetInstance().InputDetectionActive = false;
        yield return new WaitForSeconds(delayTime);
        float t = 0;
        Color color = screen.color;
        while (t <= fadeTime)
        {
            float a = Mathf.Lerp(1, 0, t / fadeTime);
            screen.color = new Color(color.r, color.g, color.b, a);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        screen.color = new Color(color.r, color.g, color.b, 0);
        InputManager.GetInstance().InputDetectionActive = true;
        Debug.Log("生成了ControlPanel");
        UIMgr.GetInstance().ShowPanel<ControlPanel>();
    }


}
