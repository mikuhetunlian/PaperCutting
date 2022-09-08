using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ParallaxElement : MonoBehaviour
{

    private CinemachineBrain _cinemachineBrain;
    private Transform _activeTransform;

    void Start()
    {
        CinemachineBrain brain =  Camera.main.gameObject.GetComponent<CinemachineBrain>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
