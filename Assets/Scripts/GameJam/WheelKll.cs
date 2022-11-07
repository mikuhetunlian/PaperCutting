using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelKll : MonoBehaviour
{
    public Transform WheelCheckPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.transform.position = WheelCheckPoint.transform.position;
            Debug.Log("Wheel÷ÿ…˙");
        }
    }
}
