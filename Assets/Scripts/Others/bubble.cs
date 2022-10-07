using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class bubble : MonoBehaviour
{
    private Transform _ferrisTransfrom;
    private CinemachineVirtualCamera _VCamera;
    private CinemachineBrain _brain;
    void Start()
    {
        _ferrisTransfrom = this.transform.parent.transform;
        _brain = Camera.main.gameObject.GetComponent<CinemachineBrain>();
        _VCamera = GameObject.Find("CM vcam4").GetComponent<CinemachineVirtualCamera>();
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag.Equals("Player"))
        { 
            ActionInBubble actionInBubble = obj.GetComponent<ActionInBubble>();
            actionInBubble.PermitAbility(true);
            actionInBubble.GetInBubble(this.transform, _ferrisTransfrom);
            CameraMgr.GetInstance().SetDefaultBlednTime(2);
            _VCamera.enabled = true;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Invoke("ResetBlendTime", 2.1f);
            _VCamera.enabled = false;
        }
    }

    private void ResetBlendTime()
    {
        _brain.m_DefaultBlend.m_Time = 0.2f;
    }
}
