using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPlatformHandle : Handle
{
    public enum Direction { Up,Down}
    public Direction direction = Direction.Up;
    public bool canHandle;



    protected override void Start()
    {
        base.Start();
        TargetAngle = -120;
    }

    protected override IEnumerator PutHandle()
    {
        _audioSource.clip = _handelFx;
        _audioSource.volume = 0.5f;
        _audioSource.Play();
        DoWhenPutHandle();
        float t = 0;
        float angle = 0;
        while (t <= ChangeTime)
        {
            if (direction == Direction.Up)
            {
                 angle = Mathf.Lerp(0, TargetAngle, t / ChangeTime);
            }
            if(direction == Direction.Down)
            {
                angle = Mathf.Lerp(TargetAngle, 0, t / ChangeTime);
            }
            _transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _canHandle = true;

        ChangeHandleDir();

    }

    protected override void DoWhenPutHandle()
    {
        if (direction == Direction.Up)
        {
            Debug.Log("Éý¸ß");
            EventMgr.GetInstance().EventTrigger("ConditionMove", true);
        }

        if (direction == Direction.Down)
        {
            Debug.Log("ÏÂ½µ");
            EventMgr.GetInstance().EventTrigger("ConditionMove", true);
        }
    }

    protected void ChangeHandleDir()
    {
        if (direction == Direction.Down)
        {
            direction = Direction.Up;
        }
        else if (direction == Direction.Up)
        {
            direction = Direction.Down;
        }

        _canHandle = true;
    }





}
