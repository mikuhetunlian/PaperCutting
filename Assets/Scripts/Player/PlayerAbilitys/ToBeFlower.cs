using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PathCreation;

public class ToBeFlower : PlayerAblity
{


    public Transform from;
    public Transform effect;
    public Transform to;
    public short bezierNum;
    public GameObject[] flowers =  new GameObject[5];
    private string PATH = "Prefab/";

    [Range(0,1)]
    public float t;

    protected override void Start()
    {
        base.Start();
        bezierNum = 12;
    }


    public void BeFlower()
    {
        ToTransparency();
        float durationTime = 1;
        for (int i = 0; i < flowers.Length; i++)
        {
            if (flowers[i] != null)
            {
                string name = flowers[i].name;
                GameObject obj = ResMgr.GetInstance().LoadRes<GameObject>(PATH + name);
                obj.transform.position = from.position;

                Vector3[] path = BezierPath();
                durationTime += Random.Range(0.25f,0.5f);

                //飞出纸屑的旋转
                obj.transform.DORotate(new Vector3(0, 0, 250), durationTime, RotateMode.WorldAxisAdd).SetId(obj.name);
                //飞出纸屑的路径
                obj.transform.DOPath(path, durationTime, PathType.CatmullRom, PathMode.Sidescroller2D).SetEase(Ease.InQuad).SetId(obj.name);
              
            }
            
        }
    }



    public void SetPathPoint(Transform effect,Transform to)
    {
        this.effect = effect;
        this.to =to;
    }

    private void ToTransparency()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }


    /// <summary>
    /// 获得bezier曲线的wayP上oints 数组
    /// </summary>
    /// <returns></returns>
    private Vector3[] BezierPath()
    {
        Vector3[] bezierPath = new Vector3[bezierNum];
        Vector3 offset  = new Vector3(0, Random.Range(-3, 3));
        for (int i = 0; i < bezierNum; i++)
        {
            float t = (float)i / (bezierNum - 1);
            bezierPath[i] = BezierPoint(t, from.position, effect.position + offset, to.position);

        }

        return bezierPath;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="t">0代表路径起点，1代表路径终点</param>
    /// <param name="pos1">起点</param>
    /// <param name="pos2">偏移点</param>
    /// <param name="pos3">终点</param>
    /// <returns></returns>
    private Vector3 BezierPoint(float t, Vector3 pos1, Vector3 pos2, Vector3 pos3)
    {
        float t1 = (1 - t) * (1 - t);
        float t2 = 2 * t * (1 - t);
        float t3 = t * t;
        return t1 * pos1 + t2 * pos2 + t3 * pos3;
    }
}
