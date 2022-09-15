using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���߳���ͬʱҲ��hitInfos��key
public enum RaycastDirection { Up, Down, Left, Right }


/// <summary>
/// ������������������߼������������С���Ƶģ�Ŀǰ��û������������صĹ��ܣ�
/// </summary>
public class PlayerController : MonoBehaviour
{
 
  

    //ÿ�������������
    public int RaycastNum;
   
    public PlayerControllerState State;
    //����player��һЩ����
    public PlayerControllerParameters Parameters;
    //player��abilitys
    protected List<PlayerAblity> abilitysList;
    //boxCollider2D
    public BoxCollider2D Collider;

    public Dictionary<RaycastDirection, List<RaycastHit2D>> hitInfos = new Dictionary<RaycastDirection, List<RaycastHit2D>>();
    public RaycastHit2D fallHitInfo;


    ///�Ƿ񻭳�����
    public bool isDrawRay;
    ///���ϱ߽翪ʼ���ϵ����߾���
    public float upRayDistance;
    ///���±߽翪ʼ���µ����߾���
    public float downRayDistance;
    ///����߽翪ʼ��������߾���
    public float leftRayDistance;
    ///���ұ߽翪ʼ���ҵ����߾���
    public float rightRayDistance;


    ///boxCollider��x��һ��
    private float _extentX;
    ///boxCollider��y��һ��
    private float _extentY;

    ///�����Ϊ����㼶������
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


    //��ʼ������
    public void Initialization()
    {
        //��Inputģ��
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
    //���ability�����
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


    //��FixUpdate��ִ�У���Զ���һЩ
    public void EveryFrame()
    {
        EarlyProcessAbilitys();
        ProcessAbiblitys();
        LateProcessAbilitys();
    }

    /// <summary>
    /// ���� abilitysList �����ϵ�����ability�е� EarlyProcessAbilitys
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
    /// ���� abilitysList �����ϵ�����ability�е�ProcessAbility
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
    /// ���� abilitysList �����ϵ�����ability�е�ProcessAbility
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
    /// �������߼������Ҫ�Ķ���
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



    //���߼�����������ĸ�������û����ײ������,���Ѷ�Ӧ�ļ����Ϣ�ӵ�hitInfos��
    //Ŀǰ ���ߵ�λ�ò�������boxCollider�ı仯���仯��ֻ�����ʼ�趨��
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
                    //��
                    for (int j = 0; j < RaycastNum; j++)
                    {
                        originPoint = new Vector2(leftUpPoint.x + j * offset_x, Collider.bounds.center.y);
                        RaycastHit2D hitInfo = DebugHelper.RaycastAndDrawLine(originPoint, Vector2.up, _extentY + upRayDistance, 1 << LayerMask.NameToLayer(_groundCheckLayerName),isDrawRay);
                        hitInfos[RaycastDirection.Up][j] = hitInfo;
                    }
                  
                    break;
                case 2:
                    //��
                    for (int j = 0; j < RaycastNum; j++)
                    {
                        originPoint = new Vector2(leftDownPoint.x + j * offset_x, Collider.bounds.center.y);
                        RaycastHit2D hitInfo = DebugHelper.RaycastAndDrawLine(originPoint, Vector2.down, _extentY + downRayDistance, 1 << LayerMask.NameToLayer(_groundCheckLayerName),isDrawRay);
                        hitInfos[RaycastDirection.Down][j] = hitInfo;
                    }
                    break;
                case 3:
                    //��
                    for (int j = 0; j < RaycastNum; j++)
                    {
                        originPoint = new Vector2(Collider.bounds.center.x, leftDownPoint.y + j * offset_y);
                        RaycastHit2D hitInfo = DebugHelper.RaycastAndDrawLine(originPoint, Vector2.left, _extentX + leftRayDistance, 1 << LayerMask.NameToLayer(_groundCheckLayerName),isDrawRay);
                        hitInfos[RaycastDirection.Left][j] = hitInfo;
                    }
                    break;
                case 4:
                    //��
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



    //�����ص�������Ծ�׼���
    private void RaycastCheckFallPoint()
    {
         fallHitInfo = DebugHelper.RaycastAndDrawLine(Collider.bounds.center, Vector2.down, 10, 1 << LayerMask.NameToLayer(_groundCheckLayerName));
    }
}
