using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : PlayerAblity
{
    /// true Ϊ ���ң�false Ϊ ���� 
    public bool IsFaceRight;
    public override void Initialization()
    {
        base.Initialization();
        IsFaceRight = true; 
        EventMgr.GetInstance().AddLinstener<KeyCode>("GetKey", GetKey);
    }


    public override void GetComponents()
    {
        base.GetComponents();
    }


    private void GetKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.A:
                IsFaceRight = false;
                break;
            case KeyCode.D:
                IsFaceRight = true;
                break;
        }
    }

    public override void ProcessAbility()
    {
        base.ProcessAbility();
        FlipDo();
    }

    /// <summary>
    /// ʵ�ַ�ת�ĵط�������IsFaceRight������ʵ�ַ�ת
    /// </summary>
    private void FlipDo()
    {
        if (IsFaceRight)
        {
            this.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) ,Mathf.Abs(transform.localScale.y));
        }
        else
        {
            this.transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
        }
    }

  
}
