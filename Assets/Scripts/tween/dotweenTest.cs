using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PathCreation;

public class dotweenTest : MonoBehaviour
{

    public Transform t1;
    public Transform t2;
    public Transform t3;
    public short bezierNum;

    [Range(0,1)]
    public float t;

    private void Awake()
    {
        bezierNum = 12;
        _rbody2D = this.GetComponent<Rigidbody2D>();
    }

    private Rigidbody2D _rbody2D;


    void Start()
    {
        //在所有tween使用前只使用一次
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
        DOTween.Clear();

        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(t1.DOMoveX(t1.position.x + 5, 2).SetEase(Ease.InOutElastic))
        //        .Append(t2.DOMoveX(t2.position.x + 5, 1).SetEase(Ease.Linear))
        //        .Append(t3.DOMoveY(t3.position.y + 6, 3).SetEase(Ease.Flash));

        Vector2[] path = BezierPath();
        _rbody2D.DOPath(path, 3, PathType.CatmullRom, PathMode.Sidescroller2D).SetEase(Ease.InSine);


    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = BezierPoint(t, new Vector2(255, 0.3f), t2.position, t3.position);
    }





    /// <summary>
    /// 获得bezier曲线的wayP上oints 数组
    /// </summary>
    /// <returns></returns>
    private Vector2[] BezierPath()
    {
        Vector2[] bezierPath = new Vector2[bezierNum];



        for (int i = 0; i < bezierNum; i++)
        {
            float t = (float)i / (bezierNum-1);
            bezierPath[i] = BezierPoint(t, t1.position, t2.position, t3.position);
                
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
    private Vector2 BezierPoint(float t,Vector2 pos1,Vector2 pos2,Vector2 pos3)
    {
        float t1 = (1 - t) * (1 - t);
        float t2 = 2 * t * (1 - t);
        float t3 = t * t;
        return t1 * pos1 + t2 * pos2 + t3 * pos3;
    }
}
