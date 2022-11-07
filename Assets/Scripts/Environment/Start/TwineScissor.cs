using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwineScissor : Scissor
{

    ///�Ƿ���֦����
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
    /// ���µ�ʱ�򣬴ݻ� ֧��ľ��
    /// </summary>
    protected override void DetectAndAttack()
    {
        EventMgr.GetInstance().EventTrigger<string>("DestorySupportBlock", "DestorySupportBlock");
    }

    /// <summary>
    /// ������֦����
    /// </summary>
    public void ReleaseScissorTwine(string info)
    {
        isTwined = false;
    }

    /// <summary>
    /// ȡ�� ��ҽ��� ������⹥��
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
