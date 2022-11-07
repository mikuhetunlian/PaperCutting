using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour,IResetAble
{
    //延时浮现的时间
    public float delayTime;
    //浮现出来的时间
    public float fadeTime;
    protected SpriteRenderer _spriteRenderer;


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke("Fade", delayTime);
    }

    protected void Fade()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeCoroutine());
    }

    protected IEnumerator FadeCoroutine()
    {
        float t = 0;
        Color color = _spriteRenderer.color;
        while (t <= fadeTime)
        {
            float a = Mathf.Lerp(0, 1, t / fadeTime);
            _spriteRenderer.color = new Color(color.r, color.g, color.b, a);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        _spriteRenderer.color = new Color(color.r, color.g, color.b, 1);
        UIMgr.GetInstance().ShowPanel<MovePanel>();
        InputUIMgr.GetInstance().SetKeyAndEvent(KeyCode.D, "DestroyMovePanel");
    }

    public void Reset()
    {
        //箭头reset，把自己透明度调回0
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0);
        //失活自己
        Debug.Log("reset箭头");
        this.gameObject.SetActive(false);
        
    }
}
