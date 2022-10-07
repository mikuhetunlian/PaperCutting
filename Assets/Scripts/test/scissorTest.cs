using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scissorTest : MonoBehaviour
{
    public GameObject scissor1;
    public GameObject scissor2;
    public GameObject scissor3;
    public GameObject scissor4;

    protected bool activeScissor;


    public void ActiveScissor()
    {
        Debug.Log("¼¤»î¼ôµ¶");
        StartCoroutine(DoActiveScissor());
    }

    protected IEnumerator DoActiveScissor()
    {
        scissor1.SetActive(true);
        yield return new WaitForSeconds(1f);
        scissor2.SetActive(true);
        yield return new WaitForSeconds(1);
        scissor3.SetActive(true);
        yield return new WaitForSeconds(1f);
        scissor4.SetActive(true);
        yield return new WaitForSeconds(1f);

    }

}
