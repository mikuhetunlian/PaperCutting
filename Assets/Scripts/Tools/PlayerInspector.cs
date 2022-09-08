using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Player))]
[CanEditMultipleObjects]
public class PlayerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Player player = target as Player;
        if (player.Movement !=null)
        {
            EditorGUILayout.LabelField("MovePreState", player.Movement.PerviousState.ToString());
            EditorGUILayout.LabelField("MovementState", player.Movement.CurrentState.ToString());
            EditorGUILayout.LabelField("ConditionPreState", player.Condition.PerviousState.ToString());
            EditorGUILayout.LabelField("ConditionState", player.Condition.CurrentState.ToString());
        }

        DrawDefaultInspector();
       
    }
}
