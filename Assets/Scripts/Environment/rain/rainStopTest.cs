using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainStopTest : MonoBehaviour
{

    public GameObject FallRain;
    private Animator[] _animators;
    private bool _isStop;
   

    void Start()
    {
        _animators = this.GetComponentsInChildren<Animator>();      
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !_isStop)
        {
            _isStop = true;
            for (int i = 0; i < _animators.Length; i++)
            {
                _animators[i].SetBool("stop", true);
            }
          
        }
    }

    public void ActiveFallRain()
    {
        if (!FallRain.activeInHierarchy)
        {
            FallRain.SetActive(true);
            Debug.Log("ActiveFallRain");
        }
    }
}
