using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 设置Animator的helper类
/// </summary>
public class AnimatorHelper 
{
    //如果animator中有这个类型的参数的话，就添加到paramaterList中
    public static void AddAnimatorParamaterIfExists(Animator animator,string paramaterName,AnimatorControllerParameterType type,List<string> paramaterList)
    {
        if (animator.HasParameterOfType(paramaterName, type))
        {
            paramaterList.Add(paramaterName);
        }
    }
    

    /// <summary>
    /// 检查parameterList是否含有 parameterName 这个参数，如果有，就在每一个loop将animator中的参数设为value
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="parameterName"></param>
    /// <param name="value"></param>
    /// <param name="parameterList"></param>
    public static void UpdateAnimatorBool(Animator animator, string parameterName, bool value, List<string> parameterList)
    {
        if (animator == null)
        {
            return;
        }

        if (parameterList.Contains(parameterName))
        {
            animator.SetBool(parameterName, value);
        }

    }





    
}
