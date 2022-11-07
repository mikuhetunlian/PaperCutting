using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyDestruter : MonoBehaviour
{

    public Transform butterflyCreater;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("butterflyUp"))
        {
            Debug.Log("¼ì²âµ½");
            collision.transform.position = new Vector3(collision.gameObject.transform.position.x,butterflyCreater.position.y, 0);
        }
    }
}
