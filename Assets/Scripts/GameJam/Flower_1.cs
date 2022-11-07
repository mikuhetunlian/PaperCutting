using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_1 : MonoBehaviour
{
    protected PlayerController _playerController;
    protected Vector3 _originPos = default(Vector3);

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.GravityActive(false);
        _originPos = this.transform.position;
        MusicMgr.GetInstance().bkValue = 0.8f;
        MusicMgr.GetInstance().PlayBkMusic("Background",true);
        Debug.Log("≤•∑≈¡À±≥æ∞“Ù¿÷");
    }

    private void OnEnable()
    {
        if (_originPos != default(Vector3))
        {
            this.transform.localPosition = _originPos;
           
        }
    
    }
}
