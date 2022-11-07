using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    public Button btnStart;
    public Button btnExit;

    protected override void Awake()
    {
        base.Awake();
        Cursor.visible = true;
        btnStart.onClick.AddListener(BtnStartEvnet);
        btnExit.onClick.AddListener(BtnExitEvnet);
    }

    public override void ShowMe()
    {
        base.ShowMe();
    }

    public void BtnStartEvnet()
    {
        Debug.Log("BtnStartEvnet");
        SceneMgr.GetInstance().LoadScene("GameJamScene");
    }

    public void BtnExitEvnet()
    {
        Debug.Log("BtnExitEvnet");
        Application.Quit();
    }


}
