using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class PaperButterfly : MonoBehaviour
{
    ///��������ߵĳ�����ұߵĳ��
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

    ///�Ƿ��һ��flip�˳��
    protected bool _isFlip;
    protected bool _isFlipLeft;
    protected bool _isFlipRight;
    protected string _flipAnimatorParameter = "flip";
    ///Ŀǰײ���� ��ɵļ��� ������
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
    /// �����������Ҫ���ĳ������һ��
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
    /// �ڲ����� ����ߵĻ����ұߵĳ��
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
    /// ���ó��Ĳ㼶˳��
    /// </summary>
    /// <param name="firstFlipWingDir">��һ����ת�ĳ��ķ�λ�����������ҳ��</param>
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
    /// �������
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
    /// �����߶���ת�꣬�е�һ������ײ������ʱ����ԭλ������ ֽ����_���� Ȼ�������Լ�
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
    /// ����Ƿ�ײ����scissor
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
