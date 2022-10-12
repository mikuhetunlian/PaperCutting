using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainRoot : MonoBehaviour
{
    public GameObject umbrella;
    public GameObject umbrellaHandle;
    public GameObject FallRain;
    ///rain射线检测的层级
    public LayerMask DetectLayerMask;
    ///射线检测的条数
    public int RayNum = 30;
    public List<PaperBridgeHandle> _paperBrigdeList = new List<PaperBridgeHandle>();
    protected BoxCollider2D _collider;
    protected Vector2 leftTopPos;
    protected Vector2 rightTopPos;
    protected float _rayLength;
    protected RaycastHit2D[] hitinfos;
    protected string BloodRainPath = "Prefab/BloodRain/blood_rain";
    protected bool isDetectPlayer;
    protected bool isCreateRainBlood;
    ///射线检测player所在 hitinfos 中的下标
    protected int detectPlayerIndex;
    ///是否停止下雨检测了
    protected bool isStopRainDetect;



    private void Start()
    {
        Initilization();
    }

    protected void Initilization()
    {
        Physics2D.queriesStartInColliders = false;
        _collider = GetComponent<BoxCollider2D>();
        leftTopPos = _collider.bounds.center + new Vector3(-_collider.bounds.extents.x, _collider.bounds.extents.y);
        rightTopPos = _collider.bounds.center + new Vector3(_collider.bounds.extents.x, _collider.bounds.extents.y);
        _rayLength = _collider.bounds.size.y;
        hitinfos = new RaycastHit2D[RayNum];
        EventMgr.GetInstance().AddLinstener<Collider2D>("RainDeathCallback", RainDeathCallback);
    }

    private void Update()
    {
        if (!isStopRainDetect)
        {
            Detect();
        }
       
    }

    /// <summary>
    /// 射线检测玩家
    /// </summary>
    protected void Detect()
    {
        if (hitinfos.Length != RayNum)
        {
            hitinfos = new RaycastHit2D[RayNum];
        }

        isDetectPlayer = false;
        detectPlayerIndex = 0;

        for (int i = 0; i < RayNum; i++)
        {
            Vector2 originPos = Vector2.Lerp(leftTopPos, rightTopPos, (float)i / (RayNum - 1));
            RaycastHit2D hitinfo = DebugHelper.RaycastAndDrawLine(originPos, Vector2.down, _rayLength, DetectLayerMask);
            hitinfos[i] = hitinfo;
            Collider2D collider = hitinfo.collider;
            if (collider != null && collider.gameObject.tag.Equals("Player"))
            {
                isDetectPlayer = true;
                detectPlayerIndex = i;
            }
        }

        if (isDetectPlayer && !isCreateRainBlood)
        {
            StopAllCoroutines();
            StartCoroutine(CreateRainBlood(hitinfos[detectPlayerIndex].collider));
            StartCoroutine(Timer(hitinfos[detectPlayerIndex].collider));
        }
    }

    /// <summary>
    /// 计时产生rainBlood的协程
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    protected IEnumerator CreateRainBlood(Collider2D collider)
    {
        isCreateRainBlood = true;
        while (isDetectPlayer)
        {
            if (collider == null)
            {
                isCreateRainBlood = false;
                yield break;
            }

            GameObject rainBlood = ResMgr.GetInstance().LoadRes<GameObject>(BloodRainPath + Random.Range(0, 4).ToString());
            rainBlood.transform.position = collider.transform.position;
            yield return new WaitForSeconds(0.35f);
        }
        isCreateRainBlood = false;
    }

    protected IEnumerator Timer(Collider2D collider)
    {

        float t = 0;
        while (t <= 2.5f)
        {
            if (!isDetectPlayer)
            {
                isCreateRainBlood = false;
                yield break;
            }
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Health health = collider.GetComponent<Health>();
        health.Damage(health.MaximumHealth, Health.DeathStyle.DeathwithoutFlower,()=>
        {
            EventMgr.GetInstance().EventTrigger<Collider2D>("RainDeathCallback", collider);
        });

    }

    /// <summary>
    /// 在雨中死了后复活的回调
    /// </summary>
    /// <param name="collider"></param>
    public void RainDeathCallback(Collider2D collider)
    {
        PlayerController playerController = collider.GetComponent<PlayerController>();
        playerController.GravityActive(true);
        foreach (PaperBridgeHandle ph in _paperBrigdeList)
        {
            ph.ResetBrigde();
        }
        umbrella.GetComponent<PathMovement>().ResetPath();
        umbrellaHandle.GetComponent<UmbrellaHandle>().ResetHandleState();
        Debug.Log("重力应用");
    }

    /// <summary>
    /// 动画事件 当下雨结束的时候激活下落雨滴
    /// </summary>
    public void ActiveRainFall()
    {
        if (FallRain == null || FallRain.activeInHierarchy)
        {
            return;
        }
        MusicMgr.GetInstance().StopBkMusic();
        FallRain.SetActive(true);
        Debug.Log("ActiveRainFall desu");
    }

    /// <summary>
    /// 下雨停止检测玩家
    /// </summary>
    public void StopDetect()
    {
        isStopRainDetect = true;
        StopAllCoroutines();
    }
}
