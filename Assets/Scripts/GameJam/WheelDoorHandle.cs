using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class WheelDoorHandle : Handle
{

    protected override IEnumerator PutHandle()
    {
        _audioSource.clip = _handelFx;
        _audioSource.volume = 0.5f;
        _audioSource.Play();
        float t = 0;
        DoWhenPutHandle();
        while (t <= ChangeTime)
        {
            float angle = Mathf.Lerp(0, TargetAngle, t / ChangeTime);
            _transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(ResetHandle());
    }


    protected override void DoWhenPutHandle()
    {
        EventMgr.GetInstance().EventTrigger<float>("ActiveDoor", ResetTime);
    }
}
