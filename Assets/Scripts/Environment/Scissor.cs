using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Scissor : MonoBehaviour
{
    ///���Ƽ�����animator���
    private Animator _aniamator;
    ///������Կ���ֻ�ܵ����ֳ���Ӧanimator���е�spine event
    [SpineEvent]
    public string eventName;


    ///�����û�� "����" MeI ��box����scene�е���box�ͻ��Ӧ�ĵ���overlap �� gizmos
    public BoxCollider2D detectBox;
    private Vector2 center;
    private Vector2 radius;

    void Start()
    {
        _aniamator = GetComponentInParent<Animator>();
        center = detectBox.bounds.center;
        radius = new Vector2(detectBox.bounds.extents.x, detectBox.bounds.extents.y);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            _aniamator.SetBool("isCut", true);
            Invoke("ResetAnimatorParameter", 1);
        }
    }

    /// <summary>
    /// ����animator����
    /// </summary>
    private void ResetAnimatorParameter()
    {
        _aniamator.SetBool("isCut", false);
    }


    /// <summary>
    /// �������е���һ˲�䴥����callback������ spine event
    /// </summary>
    public void DetectAndAttack()
    {
        Debug.Log("DetectAndAttack");
        Collider2D[] colliders =  Physics2D.OverlapBoxAll(center, radius, 0);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject.tag.Equals("Player"))
            {
                Debug.Log("DetectAndAttack player");
                GameObject blood =  ResMgr.GetInstance().LoadRes<GameObject>("Prefab/leafBlood");
                blood.transform.position = center;
                colliders[i].gameObject.GetComponent<ToBeFlower>().BeFlower();
            }
        }
    }


    /// <summary>
    /// ��ѡ���С���������ʱ��ử����Ӧ�ļ�ⷽ��
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, radius * 2);
    }

}
