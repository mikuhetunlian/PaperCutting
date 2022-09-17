using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PathMovement),true)]
[InitializeOnLoad]
public class PathMovementEditor : Editor
{
    public PathMovement pathMovementTarget
    {
        get 
        {
            return target as PathMovement;
        }
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (pathMovementTarget.AcclerationType == PathMovement.PossibleAccelerationType.AnimationCurve)
        {
            DrawDefaultInspector();
        }
        else
        {
            DrawPropertiesExcluding(serializedObject, new string[] { "Accleration" });
        }
        serializedObject.ApplyModifiedProperties();
    }


    protected virtual void OnSceneGUI()
    {
        Handles.color = Color.green;
        PathMovement t = pathMovementTarget;

        if (!t.GetOriginalTransformPotionStatus())
        {
            return;
        }

        for (int i = 0; i < t.PathElements.Count; i++)
        {
            EditorGUI.BeginChangeCheck();

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.cyan;
            Handles.Label(t.GetOriginalTransformPostion() + t.PathElements[i].PathElementPotion
                         + (Vector3.down * 0.4f) + (Vector3.right * 0.4f), "" + i);

            Vector3 oldPoint = t.GetOriginalTransformPostion() + t.PathElements[i].PathElementPotion;
            Vector3 newPoint =  Handles.FreeMoveHandle(oldPoint, Quaternion.identity,1.5f, new Vector3(0.25f, 0.25f, 0.25f), Handles.CircleHandleCap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                t.PathElements[i].PathElementPotion = newPoint - t.GetOriginalTransformPostion();
            }
        }

    }










}
