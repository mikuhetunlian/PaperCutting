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
        EditorGUILayout.HelpBox("PlayerController 解决的是player的碰撞检测和abilitys的执行",MessageType.Info);
        PlayerController controller = target as PlayerController;
        if (controller.State != null)
        {
            EditorGUILayout.LabelField("IsOnGround", controller.State.IsGrounded.ToString());
            EditorGUILayout.LabelField("isFalling", controller.State.IsFalling.ToString());
            EditorGUILayout.LabelField("isCollidingBelow", controller.State.isCollidingBelow.ToString());
            EditorGUILayout.LabelField("BelowColliderObject", controller.BelowColliderGameobject == null ? "Null" : controller.BelowColliderGameobject.name);
            EditorGUILayout.LabelField("isCollidingAbove", controller.State.isCollidingAbove.ToString());
            EditorGUILayout.LabelField("isCollidingLeft", controller.State.isCollidingLeft.ToString());
            EditorGUILayout.LabelField("isCollidingRight", controller.State.isCollidingRight.ToString());
        }


        DrawDefaultInspector();
    }
}
