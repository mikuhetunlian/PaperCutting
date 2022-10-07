using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkLine : MonoBehaviour
{
    ///两次攻击之间的间隔
    public float deltaTime;
    ///单次攻击检测的时间
    public float detectTime;
    ///是否处于检测状态
    protected bool _canAtkDetect;
    protected bool _isAtkDetect;

    protected SpriteRenderer _spriteRenderer;
    protected Color atkColor = new Color(0.5f, 0.156f, 0.18f);
    protected Color idleColor = new Color(0.31f,0.585f,0.273f);

    void Start()
    {
        Initilaization();
        StartCoroutine(AtkDetecteRoutine());
    }

    protected void Initilaization()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    
    protected IEnumerator AtkDetecteRoutine()
    {
        float t = 0;
        while (true)
        {
            t = 0;
            //这段时间开启检测
            _canAtkDetect = true;
            _spriteRenderer.color = atkColor;
            while (t <= detectTime)
            {
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _spriteRenderer.color = idleColor;

            _canAtkDetect = false;
            _isAtkDetect = false;
            yield return new WaitForSeconds(deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_canAtkDetect && !_isAtkDetect && collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("受伤");
            _isAtkDetect = true;
        }
    }


}
