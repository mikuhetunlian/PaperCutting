using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class ScissorFly : MonoBehaviour
{


    public PathCreator path;
    public float speed;
    protected float _travelled;
    /// 根据路径返回的四元数进行旋转校正
    protected float x;
    protected float y;
    protected float z;


    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        y = 90;
        z = -98;
    }

    // Update is called once per frame
    void Update()
    {
        _travelled += speed * Time.deltaTime;
        transform.position = path.path.GetPointAtDistance(_travelled, EndOfPathInstruction.Loop);
        transform.rotation = path.path.GetRotationAtDistance(_travelled, EndOfPathInstruction.Loop);
        transform.rotation *= Quaternion.AngleAxis(y, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(x, Vector3.left);
        transform.rotation *= Quaternion.AngleAxis(z, Vector3.forward);
        
    }
}
