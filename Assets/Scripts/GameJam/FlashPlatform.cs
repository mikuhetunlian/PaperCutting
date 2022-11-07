using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashPlatform : MonoBehaviour
{
    public enum IonType { Mg,Ca,S,K,N,P};

    public float delayTime;
    //平台生效的时间
    public float existTime;
    //平台生效之间的间隔时间
    public float deltaTime;
    //产生的类型名字
    public IonType ionType;


    protected BoxCollider2D _collider;
    protected SpriteRenderer _spriteRenderer;
    protected GameObject _collionObj;
    Vector2 leftOrign;
    Vector2 rightOrign;
    float rayNum;
    float rayLength;
    protected bool canDetecte;
    protected bool isCreateIon;
    protected string prefabPath = "GameJam/Prefab/";

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        leftOrign = _collider.bounds.center - new Vector3(_collider.bounds.extents.x, 0, 0);
        rightOrign = _collider.bounds.center + new Vector3(_collider.bounds.extents.x, 0, 0);
        rayNum = 10;
        rayLength = _collider.bounds.extents.y + 0.1f;
        Physics2D.queriesStartInColliders = false;
        StartCoroutine(FlashRoutine());
        Debug.Log(IonType.N.ToString());
    }

    void Update()
    {
        DetectPlayer();
    }
    protected void DetectPlayer()
    {
        if (!canDetecte)
        {
            return;
        }

        bool isDetected = false;
        for (int i = 0; i < rayNum; i++)
        {
            Vector2 orrgin = Vector2.Lerp(leftOrign, rightOrign, (float)i / (rayNum - 1));
            RaycastHit2D hitInfo = DebugHelper.RaycastAndDrawLine(orrgin, Vector2.up, rayLength, LayerMgr.PlayerLayerMask);
            if (hitInfo.collider != null)
            {
                isDetected = true;
                _collionObj = hitInfo.collider.gameObject;
            }
        }


        if (!isDetected)
        {
            isCreateIon = false;
        }

        if (isDetected && !isCreateIon)
        {
            GameObject ion = ResMgr.GetInstance().LoadRes<GameObject>(prefabPath + ionType.ToString());
            ion.transform.position = new Vector3(transform.position.x,_collionObj.transform.position.y,transform.position.z);
            isCreateIon = true;
        }

    }

    protected IEnumerator FlashRoutine()
    {
        _collider.enabled = false;
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0);
        yield return new WaitForSeconds(delayTime);

        while (true)
        {
            yield return new WaitForSeconds(existTime);
            canDetecte = false;
            _collider.enabled = false;
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0);
            yield return new WaitForSeconds(deltaTime);
            canDetecte = true;
            _collider.enabled = true;
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
        }
    }
}
