using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Spine;
using Spine.Unity;
public class test : MonoBehaviour
{
   public SkeletonAnimation skeletonAnimation;
   public SkeletonMecanim skeletonMecanim;
    public Skeleton skeleton;
    void Start()
    {
        //skeletonAnimation = GetComponent<SkeletonAnimation>();
        //skeletonMecanim = GetComponent<SkeletonMecanim>();
        //skeleton = skeletonMecanim.skeleton;
        //skeleton.SetSkin("shadowPlay");
        StartCoroutine(desu());
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private IEnumerator desu()
    {
        while (true)
        {
          
            yield return new WaitForSeconds(1);
            Debug.Log("1");
            yield return new WaitForSeconds(1);
            Debug.Log("2");
            yield return new WaitForSeconds(1);
            Debug.Log("3");
        }
    }
}
