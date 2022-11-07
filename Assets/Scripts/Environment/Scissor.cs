using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;



/// <summary>
/// Scissor 的基类
/// </summary>
public class Scissor : MonoBehaviour
{
    ///控制剪刀的animator组件
    protected Animator _aniamator;
    ///这个特性可以只能的显现出对应animator含有的spine event
    [SpineEvent]
    public string eventName;


    ///检测有没有 "剪到" MeI 的box，在scene中调整box就会对应的调整 overlap 和 gizmos
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
    /// 重置 Cut 的Animator参数 ,spine 动画事件
    /// </summary>
    protected virtual void CutOver()
    {
        _aniamator.SetBool("isCut", false);
    }

    /// <summary>
    /// 重置 twine 的Animator参数，spine 动画事件
    /// </summary>
    protected virtual void TwineOver()
    {
        _aniamator.SetBool("isTwine", false);
    }



    /// <summary>
    /// 提供外部调用，使剪刀开剪
    /// </summary>
    public virtual void Cut()
    {
        _aniamator.SetBool("isCut", true);
    }


    /// <summary>
    /// 剪刀剪中的那一瞬间触发的callback，来自 spine event
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
    /// 一直画出出对应的检测方框
    /// </summary>
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, radius * 2);
    }

}
