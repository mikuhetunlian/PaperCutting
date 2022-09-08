using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _mainVCamera;
    private CinemachineVirtualCamera _vCamera;
    private string _playerName;
    private void Awake()
    {
        _mainVCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        _vCamera = this.GetComponent<CinemachineVirtualCamera>();
        _playerName = "MeI";
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.name.Equals(_playerName))
        {
            Debug.Log("½øÈë");
            _mainVCamera.enabled = false;
            _vCamera.enabled = true;
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.name.Equals(_playerName))
        {
            Debug.Log("Àë¿ª");
            _mainVCamera.enabled = true;
            _vCamera.enabled = false;
        }
       
    }
}
