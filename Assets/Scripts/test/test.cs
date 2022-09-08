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
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonMecanim = GetComponent<SkeletonMecanim>();
        skeleton = skeletonMecanim.skeleton;
        skeleton.SetSkin("shadowPlay");
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
