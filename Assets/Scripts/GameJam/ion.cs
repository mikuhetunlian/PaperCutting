using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ion : MonoBehaviour
{
    /// <summary>
    /// 动画事件，销毁自己
    /// </summary>
    public void Destory()
    {
        GameObject.Destroy(this.gameObject);
    }
}
