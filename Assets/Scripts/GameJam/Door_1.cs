using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_1 : MonoBehaviour
{
    public AudioClip doorFx;
    protected AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        EventMgr.GetInstance().AddLinstener<float>("PlayDoorFx", PlayDoorFx);
    }



    public void PlayDoorFx(float value)
    {
        _audioSource.clip = doorFx;
        _audioSource.volume = value;
        _audioSource.Play();
    }
}
