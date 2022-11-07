using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Shower : MonoBehaviour,IResetAble
{
    public GameObject Flower_2;
    public GameObject Door;
    public AudioClip rainFx;
    protected GameObject showerWater;
    //player在雨中的计时
    protected float playerInWaterTime;
    protected bool playerInWater;
    protected GameObject _collison;
    ///是否创造出了Flower_2 重置游戏时需要被reset
    protected bool isCreateFlower_2;
    protected AudioSource _audioSource;
    

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        showerWater = this.transform.GetChild(0).gameObject;
        EventMgr.GetInstance().AddLinstener<float>("GiveWater", GiveWater);
    }

    /// <summary>
    /// "GiveWater"事件调用的函数
    /// </summary>
    /// <param name="time"></param>
    public void GiveWater(float time)
    {
        StartCoroutine("GiveWaterCoroutine", time);
    }

    protected IEnumerator GiveWaterCoroutine(float time)
    {
        _audioSource.clip = rainFx;
        _audioSource.volume = 0.5f;
        _audioSource.Play();

        float t = 0;
        playerInWaterTime = 0;
        showerWater.SetActive(true);
        while (t <= time)
        {
            if (playerInWater)
            {
                playerInWaterTime += Time.deltaTime;
            }

            if (playerInWaterTime >= 2.9 && _collison != null && !isCreateFlower_2)
            {
                Flower_2.SetActive(true);
                Flower_2.transform.position = _collison.transform.position;
                //这个_collison不参与到reset中去
                _collison.SetActive(false);
                CinemachineBrain _brain = Camera.main.GetComponent<CinemachineBrain>();
                CinemachineVirtualCamera cv = _brain.ActiveVirtualCamera as CinemachineVirtualCamera;
                cv.Follow = Flower_2.transform;
                isCreateFlower_2 = true;
                Door.GetComponent<PathMovement>().CanMove = true;
                EventMgr.GetInstance().EventTrigger<float>("PlayDoorFx", 0.5f);


            }
            t += Time.deltaTime;
            yield return null;
        }
        _audioSource.Stop();
        showerWater.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")  && collision.gameObject.name.Equals("flower_1"))
        {
            playerInWater = true;
            _collison = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && collision.gameObject.name.Equals("flower_1"))
        {
            playerInWater = false;
            _collison = null;
        }
    }

    public void Reset()
    {
        //可以再次reset Create flower2
        isCreateFlower_2 = false;
        Debug.Log("reset 花洒");
    }
}
