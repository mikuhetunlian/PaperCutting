using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwineScissor : Scissor
{

    ///ÊÇ·ñ±»Ê÷Ö¦²øÈÆ
    protected bool isTwined = true;

    protected override void Start()
    {
        base.Start();
        EventMgr.GetInstance().AddLinstener<string>("ReleaseScissorTwine", ReleaseScissorTwine);
    }

    public override void Cut()
    {
        if (isTwined)
        {
            _aniamator.SetBool("isTwine", true);
        }
        else
        {
            _aniamator.SetBool("isCut", true);
        }
    }

    /// <summary>
    /// ¼ôÏÂµÄÊ±ºò£¬´Ý»Ù Ö§³ÅÄ¾¿é
    /// </summary>
    protected override void DetectAndAttack()
    {
        EventMgr.GetInstance().EventTrigger<string>("DestorySupportBlock", "DestorySupportBlock");
    }

    /// <summary>
    /// ÍÑÀëÊ÷Ö¦²øÈÆ
    /// </summary>
    public void ReleaseScissorTwine(string info)
    {
        isTwined = false;
    }

    /// <summary>
    /// È¡Ïû Íæ¼Ò½øÈë ¼ôµ¶¼ì²â¹¥»÷
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
