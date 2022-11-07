using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;


public class  RainBlood : MonoBehaviour
{

    /// <summary>
    /// ����rainblood��ʱ�����player���ƶ��ƶ����������Ĳ�����λ��
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
    /// �����¼�
    /// </summary>
    public void Destory()
    {
        GameObject.Destroy(this.gameObject);
    }



}
