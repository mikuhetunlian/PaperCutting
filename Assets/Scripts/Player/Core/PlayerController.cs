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

    public Player _player;
    public PlayerControllerState State;
    //表明player的一些参数
    public PlayerControllerParameters Parameters;
    //boxCollider2D
    public BoxCollider2D Collider;

    public Dictionary<RaycastDirection, List<RaycastHit2D>> hitInfos = new Dictionary<RaycastDirection, List<RaycastHit2D>>();
    public RaycastHit2D fallHitInfo;


    public Player.FacingDirections facingDir = Player.FacingDirections.Right;
    public Vector2 Speed { get; protected set; }
 
    ///是否画出射线
    public bool isDrawRay;
    public short NumberOfHorizontalRays = 8;
    public short NumberOfVerticalRays = 8;
    ///距离边界一小段的offset
    public float rayOffset;
    ///从上边界开始向上的射线距离
    public float aboveRayDistance;
    ///从下边界开始向下的射线距离
    public float downRayDistance;
    ///从左边界开始向左的射线距离
    public float leftRayDistance;
    ///从右边界开始向右的射线距离
    public float rightRayDistance;

    public GameObject StandingOn;
    public Collider2D StandingOnCollider;
    public RaycastHit2D LeftHitInfo;
    public RaycastHit2D RighthHitInfo;

    ///时刻反应正在下方检测的物体是谁
    public GameObject BelowColliderGameobject;

    ///boxCollider的x的一半
    private float _extentX;
    ///boxCollider的y的一半
    private float _extentY;

    ///检测作为地面层级的名字
    private string _groundCheckLayerName;


    /// speed 的 private 引用
    protected Vector2 _speed;
    protected Vector2 _externalForce;
    protected float _currentGravity;
    protected bool _gravityActive = true;
    public Vector2 _newPostion;
    protected Transform _transform;
    protected RaycastHit2D[] _rightHitStorage;
    protected RaycastHit2D[] _leftHitStorage;
    protected RaycastHit2D[] _aboveHitStorage;
    protected RaycastHit2D[] _belowHitStorage;
    protected Vector2 _verticalRayCastFromLeft;
    protected Vector2 _verticalRayCastToRight;
    protected Vector2 _horizontalRayCastFromDown;
    protected Vector2 _horizontalRayCastToUp;
    protected InputManager _inputManager;



    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
        GameObject.DontDestroyOnLoad(this.gameObject);
        SetRaycastParameter();

    }


    private void Start()
    {
        GetComponents();
        Initialization();
    }
    //初始化数据
    public void Initialization()
    {
        //打开Input模块
        InputMgr.GetInstance().InputEnable(true);
        //初始levelMgr
        LevelManager.GetInstance();
        Parameters = new PlayerControllerParameters();
        State = new PlayerControllerState();
        rayOffset = 0.1f;
        aboveRayDistance = 0.1f;
        downRayDistance = 0.4f;
        leftRayDistance = 0.3f;
        rightRayDistance = 0.3f;
        RaycastNum = 5;
        isDrawRay = true;
        _extentX = Collider.bounds.extents.x;
        _extentY = Collider.bounds.extents.y;
        _groundCheckLayerName = "mid1";
        _aboveHitStorage = new RaycastHit2D[NumberOfVerticalRays];
        _belowHitStorage = new RaycastHit2D[NumberOfVerticalRays];
        _leftHitStorage = new RaycastHit2D[NumberOfHorizontalRays];
        _rightHitStorage = new RaycastHit2D[NumberOfHorizontalRays];
        _inputManager = _player.LinkedInputManager;
    }

    public void GetComponents()
    {
        Collider = GetComponent<BoxCollider2D>();
        _transform = GetComponent<Transform>();
        _player = GetComponent<Player>();
    }

  

    public void AddForce(Vector2 force)
    {
        _speed += force;
        _externalForce += force;
    }

    public void AddHorizontalForce(float x)
    {
        _speed.x += x;
        _externalForce.x += x;
    }

    public void AddVertivalForce(float y)
    {
        _speed.y += y;
        _externalForce.y += y;
    }

    public void SetForce(Vector2 force)
    {
        _speed = force;
        _externalForce = force;
    }

    public void SetHorizontalForce(float x)
    {
        _speed.x = x;
        _externalForce.x = x;
    }

    public void SetVerticalForce(float y)
    {
        _speed.y = y;
        _externalForce.y = y;
    }

    
    private void FixedUpdate()
    {
        EveryFrame();
    }


    public void EveryFrame()
    {
        ApplyGravity();
        FrameInitialization();

        CastRayToLeft();
        CastRayToRight();
        CastRayAbove();
        CastRayBelow();
        ComputeNewSpeed();

        _transform.Translate(_newPostion, Space.Self);

        _externalForce.x = 0;
        _externalForce.y = 0;

    }

    public void ApplyGravity()
    {
        _currentGravity = Parameters.Gravity;
        if (_gravityActive)
        {
            _speed.y += _currentGravity * Time.deltaTime;
        }

    }

    public void CastRayToLeft()
    {
        float leftRayLength = _extentX + leftRayDistance;
       
        _horizontalRayCastFromDown = Collider.bounds.center - new Vector3(0, _extentY - rayOffset);
        _horizontalRayCastToUp = Collider.bounds.center + new Vector3(0, _extentY - rayOffset);

        float leftSamllestDistance = float.MaxValue;
        int leftSmallestIndex = 0;
        bool leftHitConnect = false;

        for (int i = 0; i < NumberOfHorizontalRays; i++)
        {
            Vector2 originPoint = Vector2.Lerp(_horizontalRayCastFromDown, _horizontalRayCastToUp, (float)i / (NumberOfHorizontalRays - 1));

                _leftHitStorage[i] = DebugHelper.RaycastAndDrawLine(originPoint,   Vector2.left, leftRayLength, 1 << LayerMask.NameToLayer(_groundCheckLayerName));
                if (_leftHitStorage[i].collider != null)
                {
                    float distance = Mathf.Abs(_leftHitStorage[i].point.x - _horizontalRayCastFromDown.x);
                    if (distance < leftSamllestDistance)
                    {
                        leftSmallestIndex = i;
                        leftSamllestDistance = distance;
                    }
                    leftHitConnect = true;
                }
            
        }

        if (leftHitConnect)
        {
            State.isCollidingLeft = true;
            LeftHitInfo = _leftHitStorage[leftSmallestIndex];
            if (_inputManager.PrimaryMovement.x < 0)
            {
                float distance = Mathf.Abs(transform.position.x - _extentX - _leftHitStorage[leftSmallestIndex].point.x);
              
                Debug.Log("distance" + distance);
                _newPostion.x = -distance;

                if (Mathf.Abs(_newPostion.x) < 0.5f)
                {
                    _newPostion.x = 0;
                }
            }
          
        }
        else
        {
            State.isCollidingLeft = false;
        }

    }

    public void CastRayToRight()
    {
       
        float rightRayLength = _extentX + rightRayDistance;


        _horizontalRayCastFromDown = Collider.bounds.center - new Vector3(0, _extentY - rayOffset);
        _horizontalRayCastToUp = Collider.bounds.center + new Vector3(0, _extentY - rayOffset);

       
        float rightSmallestDistance = float.MaxValue;
        int rightSmallestIndex = 0;
        bool rightHitConnect = false;

        for (int i = 0; i < NumberOfHorizontalRays; i++)
        {
            Vector2 originPoint = Vector2.Lerp(_horizontalRayCastFromDown, _horizontalRayCastToUp, (float)i / (NumberOfHorizontalRays - 1));

                _rightHitStorage[i] = DebugHelper.RaycastAndDrawLine(originPoint,   Vector2.right, rightRayLength, 1 << LayerMask.NameToLayer(_groundCheckLayerName));
                if (_rightHitStorage[i].collider != null)
                {
                        float distance = Mathf.Abs(_rightHitStorage[i].point.x - _horizontalRayCastFromDown.x);
                        if (distance < rightSmallestDistance)
                        {
                            rightSmallestIndex = i;
                            rightSmallestDistance = distance;
                        }
                        rightHitConnect = true;
                                    
                }
        }
       
        if (rightHitConnect)
        {
            State.isCollidingRight = true;
            RighthHitInfo = _rightHitStorage[rightSmallestIndex];
            if (_inputManager.PrimaryMovement.x > 0)
            {
                float distance = Mathf.Abs(_rightHitStorage[rightSmallestIndex].point.x - (transform.position.x + _extentX));

                _newPostion.x = distance;
                if (distance < 0.5f)
                {
                    _newPostion.x = 0;
                }
            }
        }
        else
        {
            State.isCollidingRight = false;
        }

    }
   
    public void CastRayBelow()
    {
        if (_newPostion.y < 0)
        {
            State.IsFalling = true;
        }
        else
        {
            State.IsFalling = false;
        }

        float rayLength = _extentY + downRayDistance;

        //如果正在下落，延长向下检测的射线长度
        if (_newPostion.y < 0)
        {
            rayLength += Mathf.Abs(_newPostion.y);
        }

        if (_belowHitStorage.Length != NumberOfVerticalRays)
        {
            _belowHitStorage = new RaycastHit2D[NumberOfVerticalRays];
        }



        float smallestDistance = float.MaxValue;
        int smallestDistanceIndex = 0;
        bool hitConnected = false;

        _verticalRayCastFromLeft = Collider.bounds.center - new Vector3(_extentX, 0);
        _verticalRayCastToRight = Collider.bounds.center + new Vector3(_extentX, 0);

        for (int i = 0; i < NumberOfVerticalRays; i++)
        {
            Vector2 originPoint = Vector2.Lerp(_verticalRayCastFromLeft, _verticalRayCastToRight, ((float)i / (NumberOfHorizontalRays - 1)));

            _belowHitStorage[i] = DebugHelper.RaycastAndDrawLine(originPoint, Vector2.down, rayLength, 1 << LayerMask.NameToLayer(_groundCheckLayerName));
            float distance = Mathf.Abs(_verticalRayCastFromLeft.y - _belowHitStorage[i].point.y);

            if (distance < 0.05f)
            {
                break;
            }

            if (_belowHitStorage[i].collider != null)
            {
                hitConnected = true;

                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    smallestDistanceIndex = i;
                }
            }
        }

        if (hitConnected)
        {
            StandingOn = _belowHitStorage[smallestDistanceIndex].collider.gameObject;
            StandingOnCollider = _belowHitStorage[smallestDistanceIndex].collider;

            State.IsFalling = false;
            State.isCollidingBelow = true;


            //如果有其他外力 比如 跳跃 ，那就直接应用外力计算出来的速度
            if (_externalForce.y > 0 && _speed.y > 0)
            {
                _newPostion.y = _speed.y * Time.deltaTime;
                State.isCollidingBelow = false;
            }
            else
            {
                float distance = Mathf.Abs(_belowHitStorage[smallestDistanceIndex].point.y - transform.position.y);

                _newPostion.y = - distance;
            }

            if (Mathf.Abs(_newPostion.y) < 0.05f)
            {
                _newPostion.y = 0;
            }

        }
        else
        {
            State.isCollidingBelow = false;
        }


    }

    public void CastRayAbove()
    {


        _verticalRayCastFromLeft = Collider.bounds.center - new Vector3(_extentX, 0);
        _verticalRayCastToRight = Collider.bounds.center + new Vector3(_extentX, 0);

        float rayLength = _extentY + aboveRayDistance;


        float smallestDistance = float.MaxValue;
        float smallIndex = 0;
        bool hitConnected = false;

        for (int i = 0; i < NumberOfVerticalRays; i++)
        {
            Vector2 originPoint = Vector2.Lerp(_verticalRayCastFromLeft, _verticalRayCastToRight, (float)i / (NumberOfHorizontalRays - 1));
            _aboveHitStorage[i] = DebugHelper.RaycastAndDrawLine(originPoint, Vector2.up, rayLength, 1 << LayerMask.NameToLayer(_groundCheckLayerName));

            if (_aboveHitStorage[i].collider != null)
            {
                hitConnected = true;
                float distance = Mathf.Abs(_aboveHitStorage[i].point.y - _verticalRayCastFromLeft.y);
                if (Mathf.Abs(_aboveHitStorage[i].point.y - _verticalRayCastFromLeft.y) < smallestDistance)
                {
                    smallestDistance = distance;
                    smallIndex = i;
                }
            }

        }


        if (hitConnected)
        {
            State.isCollidingAbove = true;

            if (_newPostion.y > 0)
            {
                _newPostion.y = 0;
            }

        }
    }


    public void FrameInitialization()
    {
        _newPostion = _speed * Time.deltaTime;
        State.WasGroundedLastFrame = State.isCollidingBelow;
        
    }


    public void ComputeNewSpeed()
    {
        if(Time.deltaTime > 0)
        {
            _speed = _newPostion / Time.deltaTime;
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



    /// <summary>
    /// 重置旋转
    /// </summary>
    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
