using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Flower_2 : MonoBehaviour
{
    public GameObject Flower_3;
    protected PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.GravityActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -65)
        {
            Flower_3.SetActive(true);
            Flower_3.transform.position = new Vector3(transform.position.x, Flower_3.transform.position.y, Flower_3.transform.position.z);
            CinemachineVirtualCamera cv = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
            cv.Follow = Flower_3.transform;
            InputManager.GetInstance().InputDetectionActive = true;
            Destroy(this.gameObject);
        }
    }
}
