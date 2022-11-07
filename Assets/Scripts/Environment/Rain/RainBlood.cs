using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;


public class  RainBlood : MonoBehaviour
{

    /// <summary>
    /// 产生rainblood的时候根据player的移动移动方向来更改产生的位置
    /// </summary>
    private void Awake()
    {
        Initilization();
    }

    protected void Initilization()
    {
        Player.FacingDirections dir = GameObject.FindWithTag("Player").GetComponent<Player>().CurrentFaceingDir;
        if (dir == Player.FacingDirections.Left)
        {
            this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
    }

    /// <summary>
    /// 动画事件
    /// </summary>
    public void Destory()
    {
        GameObject.Destroy(this.gameObject);
    }



}
