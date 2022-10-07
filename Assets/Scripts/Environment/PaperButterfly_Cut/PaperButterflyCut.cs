using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperButterflyCut : MonoBehaviour
{

    ///Ŀǰײ���� ��ɵļ��� ������ ����������ʱ��Ĭ��Ϊ1
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
    /// �ı� ������ ״̬
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
    /// �����ĸ�����ʱ�򣬹�һ�������Լ��� ���������
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
