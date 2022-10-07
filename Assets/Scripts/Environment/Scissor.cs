using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Scissor : MonoBehaviour
{
    ///控制剪刀的animator组件
    private Animator _aniamator;
    ///这个特性可以只能的显现出对应animator含有的spine event
    [SpineEvent]
    public string eventName;


    ///检测有没有 "剪到" MeI 的box，在scene中调整box就会对应的调整overlap 和 gizmos
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
    /// 重置animator参数
    /// </summary>
    private void ResetAnimatorParameter()
    {
        _aniamator.SetBool("isCut", false);
    }


    /// <summary>
    /// 剪刀剪中的那一瞬间触发的callback，来自 spine event
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
    /// 当选择中“剪刀”的时候会画出对应的检测方框
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, radius * 2);
    }

}
