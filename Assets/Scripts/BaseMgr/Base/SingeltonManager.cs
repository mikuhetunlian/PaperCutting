using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingeltonManager<T> : MonoBehaviour where T:MonoBehaviour
{

    private static T instance;

    public static T GetInstance()
    {
        return instance;
    }


    private void Awake()
    {
        instance = this as T;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
