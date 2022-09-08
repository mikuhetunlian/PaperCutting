using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����Animator��helper��
/// </summary>
public class AnimatorHelper 
{
    //���animator����������͵Ĳ����Ļ�������ӵ�paramaterList��
    public static void AddAnimatorParamaterIfExists(Animator animator,string paramaterName,AnimatorControllerParameterType type,List<string> paramaterList)
    {
        if (animator.HasParameterOfType(paramaterName, type))
        {
            paramaterList.Add(paramaterName);
        }
    }
    

    /// <summary>
    /// ���parameterList�Ƿ��� parameterName �������������У�����ÿһ��loop��animator�еĲ�����Ϊvalue
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
