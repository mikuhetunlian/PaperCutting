using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]
[CanEditMultipleObjects]
public class PlayerControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("PlayerController �������player����ײ����abilitys��ִ��",MessageType.Info);
        PlayerController controller = target as PlayerController;
        if (controller.State != null)
        {
            EditorGUILayout.LabelField("isCollidingBelow", controller.State.isCollidingBelow.ToString());
            EditorGUILayout.LabelField("isCollidingAbove", controller.State.isCollidingAbove.ToString());
            EditorGUILayout.LabelField("isCollidingLeft", controller.State.isCollidingLeft.ToString());
            EditorGUILayout.LabelField("isCollidingRight", controller.State.isCollidingRight.ToString());
        }


        DrawDefaultInspector();
    }
}
