using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GravityTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera CV;
    protected bool isActive;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !isActive)
        {
            collision.GetComponent<PlayerController>().GravityActive(true);
            InputManager.GetInstance().InputDetectionActive = false;
            Active();
            isActive = true;
        }
    }

    /// <summary>
    /// ¼¤»î¸úËæÉãÏñ»ú
    /// </summary>
    protected void Active()
    {
        CameraMgr.GetInstance().SetDefaultBlednTime(1.5f);
        CV.enabled = true;
    }

}
