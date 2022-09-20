using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    public GameObject FallRain;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ActiveRainFall()
    {
        if (FallRain == null || FallRain.activeInHierarchy)
        {
            return;
        }

        FallRain.SetActive(true);
        Debug.Log("ActiveRainFall desu");
    }
}
