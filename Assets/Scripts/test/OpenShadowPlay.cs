using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class OpenShadowPlay : MonoBehaviour
{
    public GameObject byubo;
    public GameObject shadowPlay;
    public GameObject top;
    private HorizontalMove _horizontalMove;
    private Animator _animator;
    private SkeletonMecanim _skeletonMecanim;
    private Skeleton _skeleton;
    private bool hasOpenShadowPlay = false;
    void Start() 
    {
        _skeletonMecanim = this.GetComponent<SkeletonMecanim>();
        _skeleton = _skeletonMecanim.skeleton;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !hasOpenShadowPlay)
        {
            _horizontalMove = collision.gameObject.GetComponent<HorizontalMove>();
            _horizontalMove.PermitAbility(false);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            hasOpenShadowPlay = true;
            Debug.Log("…Ë÷√Œ™ºŸ");

            this.gameObject.layer = LayerMask.NameToLayer("front1");
            _animator = GetComponent<Animator>();
            _animator.enabled = true;
            Invoke("ResetHor", 3f);
        }

    }


    private void ResetHor()
    {
        _horizontalMove.AbilityPermitted = true;
    }

    public void ActiveShadowPlay()
    {
        shadowPlay.SetActive(true);
    }

    public void ChangeMeISkin()
    {
        Debug.Log("ChangeMeISkin");
        GameObject Mei = GameObject.Find("MeI");
        Mei.GetComponent<ChangeSkin>().ChangeSkeletonSkin("shadowPlay");
        
    }

    public void Destory()
    {
        GameObject.Destroy(top);
        GameObject.Destroy(this.gameObject);
    }


}
