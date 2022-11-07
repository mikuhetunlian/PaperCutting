using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DoChangeScene", 2);
    }

    protected void DoChangeScene()
    {
        SceneMgr.GetInstance().LoadScene("GameJamScene");
    }

    
}
