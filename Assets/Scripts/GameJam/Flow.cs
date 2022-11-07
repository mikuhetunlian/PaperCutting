using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    public bool isFlow;
    //��ʼ������ӳ�ʱ��
    public float startDelayTime;
    //����ĳ���ʱ��
    public float durationTime;
    //����ļ��ʱ��
    public float deltaTime;
    //���󴵵ķ��� Ҳ����player�����ƶ���ˮƽ�ٶ�
    public float flowerForce;
    public List<GameObject> Flows;

    [Header("Music")]
    public AudioClip flowFx;
    protected AudioSource _audioSource;

    protected GameObject _player;
    protected HorizontalMove _horizontalMove;
    protected PlayerController _playerController;
    protected bool _detect;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        EventMgr.GetInstance().AddLinstener<string>("DestoryFlow", DetoryFlow);
        Invoke("FlowMethod", startDelayTime);
    }

    protected void FlowMethod()
    {
        StartCoroutine(FlowRoutine());
    }

    /// <summary>
    /// ����Ĺ���
    /// </summary>
    /// <returns></returns>
    protected IEnumerator FlowRoutine()
    {
        float t = 0;
        while (true)
        {
            t = 0;
            //����
            _audioSource.clip = flowFx;
            _audioSource.volume = 0.2f;
            _audioSource.Play();
            while (t <= durationTime)
            {
                _detect = true;
                if (_player != null)
                {
                    if (_playerController == null)
                    {
                        _playerController = _player.GetComponent<PlayerController>();
                    }
                    if (_horizontalMove == null)
                    {
                        _horizontalMove = _player.GetComponent<HorizontalMove>();
                    }

                    //�ر�Input
                    InputManager.GetInstance().InputDetectionActive = false;
                    //ʧ��ˮƽ�ƶ�
                    _horizontalMove.AbilityPermitted = false;

                    if (!_playerController.State.isCollidingLeft)
                    {
                        //�����ƶ�
                        _playerController.SetHorizontalForce(-flowerForce);
                        _playerController.SetVerticalForce(0);
                    }
                    else
                    {
                        _playerController.SetHorizontalForce(0);
                    }
                    Debug.Log("ʩ�������ٶ�");
                    //ȡ������
                    _playerController.GravityActive(false);
                }
                foreach (GameObject obj in Flows)
                {
                    obj.SetActive(true);
                }
                isFlow = true;
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            t = 0;


            //������
            while (t <= deltaTime)
            {

                _detect = false;
                isFlow = false;
                //��������������紵��
                if (_player != null)
                {
                    //�����뿪��
                    InputManager.GetInstance().InputDetectionActive = true;
                    //�����ˮƽ�ƶ�
                    _horizontalMove.AbilityPermitted = true;
                    //ˮƽ�ٶ�Ϊ0
                    _playerController.SetHorizontalForce(0);
                    //���»������
                    _playerController.GravityActive(true);
                }

                foreach (GameObject obj in Flows)
                {
                    obj.SetActive(false);
                }

                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            _player = null;
        }
      
    }


    /// <summary>
    /// ���ͨ�غ��ֹͣ����
    /// </summary>
    /// <param name="info"></param>
    public void DetoryFlow(string info)
    {
        StopAllCoroutines();
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && _detect)
        {
            _player = collision.gameObject;
        }
    }

}
