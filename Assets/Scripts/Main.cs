using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Main : MonoBehaviour
{
    private void Awake()
    {
        UIMgr.GetInstance().ShowPanel<BKPanel>();
        UIMgr.GetInstance().ShowPanel<StartPanel>((panel)=>
         {
             EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
             eventSystem.firstSelectedGameObject = GameObject.Find("btnStart");
             
         });
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
