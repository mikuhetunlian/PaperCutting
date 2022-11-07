using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;



/// <summary>
/// Scissor �Ļ���
/// </summary>
public class Scissor : MonoBehaviour
{
    ///���Ƽ�����animator���
    protected Animator _aniamator;
    ///������Կ���ֻ�ܵ����ֳ���Ӧanimator���е�spine event
    [SpineEvent]
    public string eventName;


    ///�����û�� "����" MeI ��box����scene�е���box�ͻ��Ӧ�ĵ��� overlap �� gizmos
    public BoxCollider2D detectBox;
    protected Vector2 center;
    protected Vector2 radius;

    protected virtual void Start()
    {
        _aniamator = GetComponentInParent<Animator>();
        center = detectBox.bounds.center;
        radius = new Vector2(detectBox.bounds.extents.x, detectBox.bounds.extents.y);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Cut();
        }
    }

    /// <summary>
    /// ���� Cut ��Animator���� ,spine �����¼�
    /// </summary>
    protected virtual void CutOver()
    {
        _aniamator.SetBool("isCut", false);
    }

    /// <summary>
    /// ���� twine ��Animator������spine �����¼�
    /// </summary>
    protected virtual void TwineOver()
    {
        _aniamator.SetBool("isTwine", false);
    }



    /// <summary>
    /// �ṩ�ⲿ���ã�ʹ��������
    /// </summary>
    public virtual void Cut()
    {
        _aniamator.SetBool("isCut", true);
    }


    /// <summary>
    /// �������е���һ˲�䴥����callback������ spine event
    /// </summary>
    protected virtual void DetectAndAttack()
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
    /// һֱ��������Ӧ�ļ�ⷽ��
    /// </summary>
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, radius * 2);
    }

}
