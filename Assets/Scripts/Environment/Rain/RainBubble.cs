using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBubble : MonoBehaviour
{
    ///玩家接触到 rainBubbleTest 后能跳跃到的高度
    public float jumpHeight;
    ///玩家接触到 rainBubbleTest 后 水平移动的距离
    public float horizontalSpeed;
    ///踩中泡泡后会触发的声音
    public AudioClip bubbleSong;
    protected AudioSource _audioSoutce;
    protected GameObject _rainBubbleEffect;
    //总开关
    protected bool _rainBubbleAddSpeed;
    //水平移动速度添加 的开关
    protected bool _addSpeed;
    //竖直移动速度添加 的开关
    protected bool _addVerticalSpeed;
    protected Collider2D _other;
    //其他 rainbubbles
    protected GameObject[] rainBubbles;


    protected virtual void OnEnable()
    {

        Invoke("AddCircleCollider", 0.5f);
        _audioSoutce = this.gameObject.AddComponent<AudioSource>();
        int parentChildCount = transform.parent.childCount;
        rainBubbles = new GameObject[parentChildCount];
        for (int i = 0; i < parentChildCount; i++)
        {
            rainBubbles[i] = transform.parent.GetChild(i).gameObject;
        }
        _rainBubbleEffect = this.transform.GetChild(0).gameObject;
    }


    /// <summary>
    /// 自动加上自动适配sprite大小的碰撞
    /// </summary>
    protected virtual void AddCircleCollider()
    {
        CircleCollider2D circleCollider2D = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;
    }


    protected virtual void Update()
    {
        if (_rainBubbleAddSpeed)
        {
            RainBubbleAddSpeed();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _other = other;
            _addVerticalSpeed = true;
            _addSpeed = true;
            _rainBubbleAddSpeed = true;
        }
    }

    /// <summary>
    /// 玩家跳到 rainBubble 上后 会自动添加速度
    /// </summary>
    protected virtual void RainBubbleAddSpeed()
    {
        PlayerController playerController = _other.gameObject.GetComponent<PlayerController>();
        
        //只在接触的一瞬间给竖直向上的速度 并切换面朝向
        if (_addVerticalSpeed)
        {
            //每次跳到bubble 上 激活 rainBubbleEffect
            _rainBubbleEffect.SetActive(true);
            MusicMgr.GetInstance().PlayMusicEffect(bubbleSong,false);
            CameraMgr.GetInstance().SetCurrentCameraOffsetY(0);
            ////把上一个 rainBubble的速度控制清除
            BreakOtherRainBubblesEffect();

            float gravity = Mathf.Abs(playerController.Parameters.Gravity);
            playerController.SetVerticalForce(Mathf.Sqrt(2 * gravity * jumpHeight));

            //根据水平速度的正负来改变相应的面朝向
            if (horizontalSpeed > 0)
            {
                playerController._player.SetFace(Player.FacingDirections.Right);
            }
            if(horizontalSpeed < 0)
            {
                playerController._player.SetFace(Player.FacingDirections.Left);
            }

            _addVerticalSpeed = false;
        }
        //如果 玩家移动操控了 取消水平速度的保持
        if (playerController._player.LinkedInputManager.PrimaryMovement.x != 0 && _addSpeed)
        {
            _addSpeed = false;
            playerController.SetHorizontalForce(0);
            _rainBubbleAddSpeed = false;
        }
        //如果 玩家落到了地面上 取消水平速度的保持并在这一瞬间设置为0
        if (playerController.State.IsGrounded)
        {
            Debug.Log("落地");
            playerController.SetHorizontalForce(0);
            _rainBubbleAddSpeed = false;
            CameraMgr.GetInstance().ResetCurrnteCameraOffset();
            return;
        }
        //如果在这 说明玩家既没有操控 也还没有落到地面  就继续保持水平的移动速度
        if (playerController._player.LinkedInputManager.PrimaryMovement.x == 0 && _addSpeed)
        {
        
            playerController.SetHorizontalForce(horizontalSpeed);
        }
    }

    protected virtual void BreakOtherRainBubblesEffect()
    {
        for (int i = 0; i < rainBubbles.Length; i++)
        {
            if (rainBubbles[i] == this.gameObject)
            {
                continue;
            }
            RainBubble rainBubble = rainBubbles[i].GetComponent<RainBubble>();
            rainBubble.BreakControl();
        }
    }

    /// <summary>
    /// 让其他 rainBubbe调用  脱离自己水平速度的控制
    /// </summary>
    public virtual void BreakControl()
    {
        _addSpeed = false;
    }
}
