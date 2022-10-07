using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkLine : MonoBehaviour
{
    ///���ι���֮��ļ��
    public float deltaTime;
    ///���ι�������ʱ��
    public float detectTime;
    ///�Ƿ��ڼ��״̬
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
            //���ʱ�俪�����
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
            Debug.Log("����");
            _isAtkDetect = true;
        }
    }


}
