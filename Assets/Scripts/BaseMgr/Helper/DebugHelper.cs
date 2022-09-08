using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugHelper 
{

    /// <summary>
    /// 射线检测并且在场景上画出射线
    /// </summary>
    /// <returns></returns>
    public static RaycastHit2D RaycastAndDrawLine(Vector3 orignPoint,Vector3 direction,float distance,int mask,bool isDrawLine = true)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(orignPoint, direction, distance, mask);
        if (isDrawLine)
        {
            if (hitInfo.collider != null)
            {
                Debug.DrawLine(orignPoint, hitInfo.point, Color.red);
            }
            else
            {
                Debug.DrawLine(orignPoint, orignPoint + direction * distance, Color.green);
            }
        }
        return hitInfo;

    }


}
