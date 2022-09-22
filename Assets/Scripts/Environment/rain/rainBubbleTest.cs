using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainBubbleTest : MonoBehaviour
{
    ///��ҽӴ��� rainBubbleTest ������Ծ���ĸ߶�
    public float jumpHeight;
    ///��ҽӴ��� rainBubbleTest �� ˮƽ�ƶ��ľ���
    public float horizontalSpeed;
    ///�������ݺ�ᴥ��������
    public AudioClip bubbleSong;
    private AudioSource _audioSoutce;
    private GameObject _rainBubbleEffect;
    //�ܿ���
    private bool _rainBubbleAddSpeed;
    //ˮƽ�ƶ��ٶ���� �Ŀ���
    private bool _addSpeed;
    //��ֱ�ƶ��ٶ���� �Ŀ���
    private bool _addVerticalSpeed;
    private Collider2D _other;
    //���� rainbubbles
    private GameObject[] rainBubbles;


    private void OnEnable()
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
    /// �Զ������Զ�����sprite��С����ײ
    /// </summary>
    private void AddCircleCollider()
    {
        CircleCollider2D circleCollider2D = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;
    }


    private void Update()
    {
        if (_rainBubbleAddSpeed)
        {
            RainBubbleAddSpeed();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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
    /// ������� rainBubble �Ϻ� ���Զ�����ٶ�
    /// </summary>
    private void RainBubbleAddSpeed()
    {
        PlayerController playerController = _other.gameObject.GetComponent<PlayerController>();
        
        //ֻ�ڽӴ���һ˲�����ֱ���ϵ��ٶ� ���л��泯��
        if (_addVerticalSpeed)
        {
            //ÿ������bubble �� ���� rainBubbleEffect
            _rainBubbleEffect.SetActive(true);
            MusicMgr.GetInstance().PlayMusicEffect(bubbleSong,false);
            CameraMgr.GetInstance().SetCurrentCameraOffsetY(0);
            ////����һ�� rainBubble���ٶȿ������
            BreakOtherRainBubblesEffect();

            float gravity = Mathf.Abs(playerController.Parameters.Gravity);
            playerController.SetVerticalForce(Mathf.Sqrt(2 * gravity * jumpHeight));

            //����ˮƽ�ٶȵ��������ı���Ӧ���泯��
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
        //��� ����ƶ��ٿ��� ȡ��ˮƽ�ٶȵı���
        if (playerController._player.LinkedInputManager.PrimaryMovement.x != 0 && _addSpeed)
        {
            _addSpeed = false;
        }
        //��� ����䵽�˵����� ȡ��ˮƽ�ٶȵı��ֲ�����һ˲������Ϊ0
        if (playerController.State.IsGrounded)
        {
            Debug.Log("���");
            playerController.SetHorizontalForce(0);
            _rainBubbleAddSpeed = false;
            CameraMgr.GetInstance().ResetCurrnteCameraOffset();
            return;
        }
        //������� ˵����Ҽ�û�вٿ� Ҳ��û���䵽����  �ͼ�������ˮƽ���ƶ��ٶ�
        if (playerController._player.LinkedInputManager.PrimaryMovement.x == 0 && _addSpeed)
        {
        
            playerController.SetHorizontalForce(horizontalSpeed);
        }
    }

    private void BreakOtherRainBubblesEffect()
    {
        for (int i = 0; i < rainBubbles.Length; i++)
        {
            if (rainBubbles[i] == this.gameObject)
            {
                continue;
            }
            rainBubbleTest rainBubble = rainBubbles[i].GetComponent<rainBubbleTest>();
            rainBubble.BreakControl();
        }
    }

    /// <summary>
    /// ������ rainBubbe����  �����Լ�ˮƽ�ٶȵĿ���
    /// </summary>
    public void BreakControl()
    {
        _addSpeed = false;
    }
}
