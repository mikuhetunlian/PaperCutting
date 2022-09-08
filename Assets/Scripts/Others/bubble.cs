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

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name.Equals("MeI"))
        {
            
            ActionInBubble actionInBubble = obj.GetComponent<ActionInBubble>();
            actionInBubble.PermitAbility(true);
            actionInBubble.GetInBubble(this.transform, _ferrisTransfrom);
                //horizontalMove.PermitAbility(false);
                //jumpAbility.PermitAbility(false);
                //keepRotateInBubble.SetBubbleTransform(this.transform);
                //keepRotateInBubble.PermitAbility(true);
                //obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.15f, this.transform.position.z);
                //obj.transform.SetParent(_ferrisTransfrom, true);
                _brain.m_DefaultBlend.m_Time = 2;
                _VCamera.enabled = true;
                //player.Movement.ChangeState(PlayerStates.MovementStates.InBubble);
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("MeI"))
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
