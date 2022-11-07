using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Wheel : MonoBehaviour
{
    public List<GameObject> platforms;
    //每帧秒转过的角度
    public float deltaAngle;
    protected float radius;
    protected Vector2 centerPoint;

    void Start()
    {
        radius = (transform.position  - platforms[0].transform.position).magnitude;
        centerPoint = new Vector2(transform.position.x, transform.position.y);
        StartCoroutine(WheelRoutine());
    }


    protected IEnumerator WheelRoutine()
    {
       
        float angle = 0;
        while (true)
        {
            if (angle >= 360)
            {
                angle = 0;
            }

            int i = 0;
            foreach (GameObject obj in platforms)
            {
                float x = centerPoint.x + radius * Mathf.Cos((angle + i * (360/platforms.Count)) * Mathf.Deg2Rad);
                float y = centerPoint.y + radius * Mathf.Sin((angle + i * (360/platforms.Count)) * Mathf.Deg2Rad);
                obj.transform.DOMove(new Vector3(x, y, obj.transform.position.z), Time.deltaTime);
                angle += deltaAngle;
                i++;
                yield return null;
            }
        }
        
    }
}
