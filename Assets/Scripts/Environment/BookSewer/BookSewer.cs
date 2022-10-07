using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSewer : MonoBehaviour
{
    ///向下砸的时间
    public float DownAtkTime;
    ///向下砸完静止的时间
    public float DownToUpStaticTime;
    ///砸完向上恢复的时间
    public float UpResetTime;
    ///恢复完距离下一次砸的间隔
    public float AtkDeltaTime;
    ///第一开始运作的延时时间
    public float StartDelayTime;
    ///产生出来钉子的大小
    public float StapleSize;

    protected BoxCollider2D _collider;
    protected Vector3 _topPostion;
    protected Vector3 _downPostion;
    protected string staplePath = "Prefab/Staple/Staple";
    private bool isDetecte;

    void Start()
    {
        Initilization();
        CalculatePostion();
        SetOriginPostion();
        StartAtkRoutine();
    }

    /// <summary>
    /// 初始化一些组件
    /// </summary>
    protected void Initilization()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// 计算 向上的位置 和 向下的位置
    /// </summary>
    protected void CalculatePostion()
    {
        _topPostion =  transform.localPosition + new Vector3(0, _collider.bounds.size.y,0);
        _downPostion = transform.localPosition;
    }

    /// <summary>
    /// 设定初始的位置
    /// </summary>
    protected void SetOriginPostion()
    {
        transform.localPosition = _topPostion;
    }

    /// <summary>
    /// 开始运作
    /// </summary>
    protected void StartAtkRoutine()
    {
        StartCoroutine(AttackRoutine());
    }   


    /// <summary>
    /// 订书机运作routine
    /// </summary>
    /// <returns></returns>
    protected IEnumerator AttackRoutine()
    {

        if (StartDelayTime != 0)
        {
            yield return new WaitForSeconds(StartDelayTime);
        }

        float t = 0;
        Vector3 originPos;
        while (true)
        {
        
            t = 0; 
            originPos = transform.localPosition;
            while (Mathf.Abs(transform.localPosition.y - _downPostion.y) >= 0.0001f)
            { 
                transform.localPosition = Vector3.Lerp(originPos, _downPostion, t / DownAtkTime);
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Dectect();
            CreateStaple();

            yield return new WaitForSeconds(DownToUpStaticTime);


            t = 0;
            originPos = transform.localPosition;

            while (Mathf.Abs(transform.localPosition.y - _topPostion.y) >= 0.0001f)
            {
                transform.localPosition = Vector3.Lerp(originPos, _topPostion, t / UpResetTime);
                t += Time.fixedDeltaTime;
                yield return null; 
            }
 

            yield return new WaitForSeconds(AtkDeltaTime);


        }

    }

    /// <summary>
    /// 每次砸下检测是否砸到了 player
    /// </summary>
    protected void Dectect()
    {
        Vector2 size = new Vector2(_collider.bounds.size.x, 5);

        Collider2D[] hitInfos =  Physics2D.OverlapBoxAll(transform.position - new Vector3(0, _collider.bounds.extents.y, 0), size, 0);

        for (int i = 0; i < hitInfos.Length; i++)
        {
            if (hitInfos[i] != null)
            {
                if (hitInfos[i].gameObject.tag.Equals("Player"))
                {
                    Debug.Log("检测到player");
                    GameObject blood = ResMgr.GetInstance().LoadRes<GameObject>("Prefab/leafBlood");
                    blood.transform.position = hitInfos[i].gameObject.transform.position;
                    Health health = hitInfos[i].gameObject.GetComponent<Health>();
                    health.Damage(10);
                }
            }
        }
    }


    /// <summary>
    /// 产生订书机钉子
    /// </summary>
    protected void CreateStaple()
    {
        GameObject obj = ResMgr.GetInstance().LoadRes<GameObject>(staplePath);
        obj.transform.position = transform.position - new Vector3(0, _collider.bounds.extents.y, 0);
        obj.transform.rotation = Quaternion.AngleAxis(Random.Range(10, 120), Vector3.forward);
        obj.transform.localScale *= StapleSize;
        Rigidbody2D rbody =  obj.GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(Random.Range(-3,3), Random.Range(20,25));
        Destroy(obj, 2);
    }



    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(_downPostion - new Vector3(0, _collider.bounds.extents.y, 0), _collider.bounds.size);
    }

}
