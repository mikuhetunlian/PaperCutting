using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class PaperButterfly : MonoBehaviour
{
    ///表明是左边的翅膀还是右边的翅膀
    public enum WingDirection { Left, Right }

    public GameObject left;
    public GameObject right;
    public GameObject leftLine;
    public GameObject rightLine;
    public bool CanActiveScissor { get { return _isFlipLeft && _isFlipRight; } }

    protected Animator _leftAnimator;
    protected Animator _rightAnimator;
    protected MeshRenderer _leftMeshRenderer;
    protected MeshRenderer _rightMeshRenderer;

    ///是否第一次flip了翅膀
    protected bool _isFlip;
    protected bool _isFlipLeft;
    protected bool _isFlipRight;
    protected string _flipAnimatorParameter = "flip";
    ///目前撞到的 会飞的剪刀 的数量
    protected int _scissorNum;
    protected bool isActiveScissor;


    void Start()
    {
        Initiliazation();
    }

    protected void Initiliazation()
    {
        EventMgr.GetInstance().AddLinstener<float>("ActiveScissor", ActiveScissor);

        if (left != null)
        {
            _leftMeshRenderer = left.GetComponent<MeshRenderer>();
            _leftAnimator = left.GetComponent<Animator>();
        }
        if (right != null)
        {
            _rightMeshRenderer = right.GetComponent<MeshRenderer>();
            _rightAnimator = right.GetComponent<Animator>();
        }
    }



    private void Update()
    {
        
        CreateButterflyCut();
    }

    /// <summary>
    /// 给出风机调用要吹的翅膀是哪一个
    /// </summary>
    /// <param name="dir"></param>
    public void FlipWing(WingDirection dir)
    {
        if (!_isFlip)
        {
            SetWingOrderInLayer(dir);
        }

        if (dir == WingDirection.Left)
        {
            Flip(_leftAnimator);
            leftLine.SetActive(false);
            _isFlipLeft = true;
        }
        if (dir == WingDirection.Right)
        {
            Flip(_rightAnimator);
            rightLine.SetActive(false);
            _isFlipRight = true;
        }


    }

    /// <summary>
    /// 内部调用 吹左边的还是右边的翅膀
    /// </summary>
    /// <param name="animator"></param>
    protected void Flip(Animator animator)
    {
        if (animator == null)
        {
            return;
        }
        animator.SetBool(_flipAnimatorParameter, true);
        _isFlip = true;
    }

    /// <summary>
    /// 设置翅膀的层级顺序
    /// </summary>
    /// <param name="firstFlipWingDir">第一个翻转的翅膀的方位，是左翅膀还是右翅膀</param>
    protected void SetWingOrderInLayer(WingDirection firstFlipWingDir)
    {
        _isFlip = true;
        if (firstFlipWingDir == WingDirection.Left)
        {
            _rightMeshRenderer.sortingOrder = _leftMeshRenderer.sortingOrder + 1;
        }
        else
        {
            _leftMeshRenderer.sortingOrder = _rightMeshRenderer.sortingOrder + 1;
        }
    }



    /// <summary>
    /// 激活剪刀
    /// </summary>
    public void ActiveScissor(float delayTime)
    {
        Invoke("DoActiveScissor", delayTime);
    }

    protected void DoActiveScissor()
    {
        if (_isFlipLeft && _isFlipRight && !isActiveScissor)
        {
            GameObject.Find("scissorTest").GetComponent<scissorTest>().ActiveScissor();
            isActiveScissor = true;
        }
    }

    /// <summary>
    /// 当两边都翻转完，有第一个剪刀撞过来的时候，在原位置生成 纸蝴蝶_被剪 然后销毁自己
    /// </summary>
    protected void CreateButterflyCut()
    {
        if (_isFlipLeft && _isFlipRight && _scissorNum == 1)
        {
            GameObject paperButterFlyCut = ResMgr.GetInstance().LoadRes<GameObject>("Prefab/PaperButterfly_cut/PaperButterfly_Cut");
            paperButterFlyCut.transform.position = this.transform.position;
            GameObject.Destroy(this.gameObject);
        }
    }



    /// <summary>
    /// 检测是否撞到了scissor
    /// </summary>
    protected void DectectButterfly(Collider2D collison)
    {
        if(collison.gameObject.tag.Equals("ScissorFly"))
        {
            _scissorNum++;
            GameObject.Destroy(collison.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DectectButterfly(collision);
    }

}
