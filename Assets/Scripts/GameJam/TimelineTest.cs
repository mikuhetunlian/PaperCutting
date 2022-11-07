using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
public class TimelineTest : MonoBehaviour
{

    public PlayableDirector playableDirector;
    public GameObject flower_3;
    public GameObject flower_4;
    protected bool haveCreateFlower4;


    private void Update()
    {
        if (flower_4.activeInHierarchy && haveCreateFlower4)
        {
            flower_4.transform.position = flower_3.transform.position;
            InputManager.GetInstance().InputDetectionActive = true;
            CinemachineVirtualCamera cv = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
          
            
            flower_3.SetActive(false);

            cv.enabled = true;
            GameObject.Destroy(this);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("flower_3") && !haveCreateFlower4)
        {
            
            
            InputManager.GetInstance().InputDetectionActive = false;
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.SetHorizontalForce(0);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("flower_3") && !haveCreateFlower4)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController.State.isCollidingBelow)
            {
                playableDirector.Play();
                haveCreateFlower4 = true;
            }
        }
    }
}
