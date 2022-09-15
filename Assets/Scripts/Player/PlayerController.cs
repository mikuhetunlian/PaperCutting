using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//射线朝向，同时也是hitInfos的key
public enum RaycastDirection { Up, Down, Left, Right }


/// <summary>
/// 这个类是用来管理射线检测和重力方向大小控制的（目前还没有重力控制相关的功能）
/// </summary>
public class PlayerController : MonoBehaviour
{
 
  

    //每个方向的射线数
    public int RaycastNum;
   
    public PlayerControllerState State;
    //表明player的一些参数
    public PlayerControllerParameters Parameters;
    //player的abilitys
    protected List<PlayerAblity> abilitysList;
    //boxCollider2D
    public BoxCollider2D Collider;

    public Dictionary<RaycastDirection, List<RaycastHit2D>> hitInfos = new Dictionary<RaycastDirection, List<RaycastHit2D>>();
    public RaycastHit2D fallHitInfo;


    ///是否画出射线
    public bool isDrawRay;
    ///从上边界开始向上的射线距离
    public float upRayDistance;
    ///从下边界开始向下的射线距离
    public float downRayDistance;
    ///从左边界开始向左的射线距离
    public float leftRayDistance;
    ///从右边界开始向右的射线距离
    public float rightRayDistance;


    ///boxCollider的x的一半
    private float _extentX;
    ///boxCollider的y的一半
    private float _extentY;

    ///检测作为地面层级的名字
    private string _groundCheckLayerName;

    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
        GameObject.DontDestroyOnLoad(this.gameObject);
        GetComponents();
        Initialization();
        SetRaycastParameter();
    }

    void Start()
    {

    }


    private void FixedUpdate()
    {
        RaycastCheckHit();
        RaycastCheckFallPoint();
    }

    private void Update()
    {
        EveryFrame();
    }


    //初始化数据
    public void Initialization()
    {
        //打开Input模块
        InputMgr.GetInstance().InputEnable(true);
        Parameters = new PlayerControllerParameters();
        State = new PlayerControllerState();
        upRayDistance = 0.1f;
        downRayDistance = 0.4f;
        leftRayDistance = 0.1f;
        rightRayDistance = 0.1f;
        RaycastNum = 5;
        isDrawRay = true;
        _extentX = Collider.bounds.extents.x;
        _extentY = Collider.bounds.extents.y;
        _groundCheckLayerName = "mid1";
    }
    //获得ability组件们
    public void GetComponents()
    {
        PlayerAblity[] abilitys = this.gameObject.GetComponents<PlayerAblity>();
        Collider = this.gameObject.GetComponent<BoxCollider2D>();
        abilitysList = new List<PlayerAblity>();
        for (int i = 0; i < abilitys.Length; i++)
        {
            abilitysList.Add(abilitys[i]);
        }

    }


    //在FixUpdate中执行，相对独立一些
    public void EveryFrame()
    {
        EarlyProcessAbilitys();
        ProcessAbiblitys();
        LateProcessAbilitys();
    }

    /// <summary>
    /// 遍历 abilitysList 来不断地运行ability中的 EarlyProcessAbilitys
    /// <summary>
    protected void EarlyProcessAbilitys()
    {
        foreach (PlayerAblity ability in abilitysList)
        {
            if (ability.AbilityPermitted)
            {
                ability.EarlyProcessAblity();
            }

        }
    }


    /// <summary>
    /// 遍历 abilitysList 来不断地运行ability中的ProcessAbility
    /// </summary>
    protected void ProcessAbiblitys()
    {
        foreach (PlayerAblity ability in abilitysList)
        {
            if (ability.AbilityPermitted)
            {
                ability.ProcessAbility();
                ability.UpdateAnimator();
            }
         
        }
    }

    /// <summary>
    /// 遍历 abilitysList 来不断地运行ability中的ProcessAbility
    /// </summary>
    protected void LateProcessAbilitys()
    {
        foreach (PlayerAblity ability in abilitysList)
        {
            if (ability.AbilityPermitted)
            {
                ability.LateProcessAbility();
            }

        }
    }






    /// <summary>
    /// 设置射线检测所需要的东西
    /// </summary>
    private void SetRaycastParameter()
    {
        hitInfos.Add(RaycastDirection.Up, new List<RaycastHit2D>());
        hitInfos.Add(RaycastDirection.Down, new List<RaycastHit2D>());
        hitInfos.Add(RaycastDirection.Left, new List<RaycastHit2D>());
        hitInfos.Add(RaycastDirection.Right, new List<RaycastHit2D>());

        for (int i = 1; i <= RaycastNum; i++)
        {
            hitInfos[RaycastDirection.Up].Add(new RaycastHit2D());
            hitInfos[RaycastDirection.Right].Add(new RaycastHit2D());
            hitInfos[RaycastDirection.Left].Add(new RaycastHit2D());
            hitInfos[RaycastDirection.Down].Add(new RaycastHit2D());
        }
    }



    //射线检测上下左右四个方向有没有碰撞到物体,并把对应的检测信息加到hitInfos中
    //目前 射线的位置不会随着boxCollider的变化而变化，只会在最开始设定好
    private void RaycastCheckHit()
    {
        Vector2 leftOrignPoint = Collider.bounds.center + Vector3.left * _extentX;
        Vector2 rightOrignPoint = Collider.bounds.center - Vector3.left * _extentX;
        Vector2 upOrignPoint = Collider.bounds.center + Vector3.up * _extentY;
        Vector2 downOrignPoint = Collider.bounds.center - Vector3.up *_extentY;

        Vector2 leftUpPoint = leftOrignPoint + Vector2.up * _extentY;
        Vector2 leftDownPoint = leftOrignPoint - Vector2.up * _extentY;
        Vector2 rightUpPoint = rightOrignPoint + Vector2.up * _extentY;
        Vector2 rightDownPoint = rightOrignPoint - Vector2.up *_extentY;



        Vector2 originPoint = new Vector2();
        float offset_x = Collider.bounds.size.x / (RaycastNum - 1);
        float offset_y = Collider.bounds.size.y / (RaycastNum - 1);

        for (int i = 1; i <= 4; i++)
        {
            switch (i)
            {
                case 1:
                    //上
                    for (int j = 0; j < RaycastNum; j++)
                    {
                        originPoint = new Vector2(leftUpPoint.x + j * offset_x, Collider.bounds.center.y);
                        RaycastHit2D hitInfo = DebugHelper.RaycastAndDrawLine(originPoint, Vector2.up, _extentY + upRayDistance, 1 << LayerMask.NameToLayer(_groundCheckLayerName),isDrawRay);
                        hitInfos[RaycastDirection.Up][j] = hitInfo;
                    }
                  
                    break;
                case 2:
                    //下
                    for (int j = 0; j < RaycastNum; j++)
                    {
                        originPoint = new Vector2(leftDownPoint.x + j * offset_x, Collider.bounds.center.y);
                        RaycastHit2D hitInfo = DebugHelper.RaycastAndDrawLine(originPoint, Vector2.down, _extentY + downRayDistance, 1 << LayerMask.NameToLayer(_groundCheckLayerName),isDrawRay);
                        hitInfos[RaycastDirection.Down][j] = hitInfo;
                    }
                    break;
                case 3:
                    //左
                    for (int j = 0; j < RaycastNum; j++)
                    {
                        originPoint = new Vector2(Collider.bounds.center.x, leftDownPoint.y + j * offset_y);
                        RaycastHit2D hitInfo = DebugHelper.RaycastAndDrawLine(originPoint, Vector2.left, _extentX + leftRayDistance, 1 << LayerMask.NameToLayer(_groundCheckLayerName),isDrawRay);
                        hitInfos[RaycastDirection.Left][j] = hitInfo;
                    }
                    break;
                case 4:
                    //右
                    for (int j = 0; j < RaycastNum; j++)
                    {
                        originPoint = new Vector2(Collider.bounds.center.x, leftDownPoint.y + j * offset_y);
                        RaycastHit2D hitInfo = DebugHelper.RaycastAndDrawLine(originPoint, Vector2.right, _extentX + rightRayDistance, 1 << LayerMask.NameToLayer(_groundCheckLayerName), isDrawRay);
                        hitInfos[RaycastDirection.Right][j] = hitInfo;
                    }
                    break;
            }
            
        }

        bool isCollider = false;
        foreach (RaycastHit2D hitInfo in hitInfos[RaycastDirection.Up])
        {
            if(hitInfo.collider!=null)
            {
                isCollider= true;
                break;
            }
        }
        State.isCollidingAbove = isCollider;

        isCollider = false;
        foreach (RaycastHit2D hitInfo in hitInfos[RaycastDirection.Down])
        {
            if (hitInfo.collider != null)
            {
                isCollider = true;
                break;
            }
        }
        State.isCollidingBelow = isCollider;

        isCollider = false;
        foreach (RaycastHit2D hitInfo in hitInfos[RaycastDirection.Left])
        {
            if (hitInfo.collider != null)
            {
                isCollider = true;
                break;
            }
        }
        State.isCollidingLeft = isCollider;

        isCollider = false;
        foreach (RaycastHit2D hitInfo in hitInfos[RaycastDirection.Right])
        {
            if (hitInfo.collider != null)
            {
                isCollider = true;
                break;
            }
        }
        State.isCollidingRight = isCollider;


    }



    //检测落地点的坐标以精准落地
    private void RaycastCheckFallPoint()
    {
         fallHitInfo = DebugHelper.RaycastAndDrawLine(Collider.bounds.center, Vector2.down, 10, 1 << LayerMask.NameToLayer(_groundCheckLayerName));
    }
}
