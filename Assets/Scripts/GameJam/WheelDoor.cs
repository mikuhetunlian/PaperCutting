using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelDoor : MonoBehaviour
{
    [Header("Music")]
    public AudioClip doorFx1;
    public AudioClip doorFx2;
    protected AudioSource _audioSource;

    protected BoxCollider2D Collider;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Collider = GetComponent<BoxCollider2D>();
        EventMgr.GetInstance().AddLinstener<float>("ActiveDoor", ActiveDoor);
    }

    public void ActiveDoor(float time)
    {
        StartCoroutine(ActiveDoorCoroutine(time));
    }

    protected IEnumerator ActiveDoorCoroutine(float time)
    {
        float changeTime = 1;
        float resetTIme = time;
        float t = 0;
        Vector2 changePos = new Vector2(transform.position.x, transform.position.y + Collider.bounds.size.y);
        Vector2 resetPos = this.transform.position;

        _audioSource.clip = doorFx1;
        _audioSource.volume = 0.5f;
        _audioSource.Play();

        while (t <= changeTime)
        {
            transform.position =  Vector2.Lerp(resetPos, changePos, t / changeTime);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        transform.position = Vector2.Lerp(resetPos, changePos,1);
        t = 0;

        while (t <= resetTIme)
        {
            transform.position = Vector2.Lerp(changePos, resetPos, t / resetTIme);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _audioSource.clip = doorFx2;
        _audioSource.volume = 0.5f;
        _audioSource.Play();

        transform.position = Vector2.Lerp(changePos, resetPos, 1);

    }

}
