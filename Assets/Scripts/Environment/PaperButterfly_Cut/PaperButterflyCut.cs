using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperButterflyCut : MonoBehaviour
{

    ///目前撞到的 会飞的剪刀 的数量 创建出来的时候默认为1
    protected int _scissorNum;
    protected Animator _animator;
    void Start()
    {
        Initilization();
    }

    protected void Initilization()
    {
        _scissorNum = 1;
        _animator = GetComponent<Animator>();
    }


    /// <summary>
    /// 改变 被剪的 状态
    /// </summary>
    protected void SetAnimatorState()
    {
        string parameterName = "state" + _scissorNum;
        _animator.SetBool(parameterName, true);

        if (_scissorNum == 4)
        {
            EventMgr.GetInstance().EventTrigger("CameraShake", 2);
            Invoke("CreatButterflyAlive", 2);
        }
    }

    /// <summary>
    /// 当够四个剪刀时候，过一会销毁自己并 创建活蝴蝶
    /// </summary>
    protected void CreatButterflyAlive()
    {
        GameObject obj = ResMgr.GetInstance().LoadRes<GameObject>("Prefab/PaperButterfly_Alive/PaperButterfly_Alive");
        obj.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("ScissorFly"))
        {
            _scissorNum++;
            GameObject.Destroy(collision.gameObject);
            SetAnimatorState();
        }
    }

}
