using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Original code by CodeMonkey: https://unitycodemonkey.com/video.php?v=7j_BNf9s0jM, Modified by Melinon Remy
[CustomEditor(typeof(SplineDone))]
public class SplineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SplineDone spline = (SplineDone)target;

        if (GUILayout.Button("Add Anchor"))
        {
            Undo.RecordObject(spline, "Add Anchor");
            spline.AddAnchor();
            spline.SetDirty();
            serializedObject.Update();
        }

        if (GUILayout.Button("Remove Last Anchor"))
        {
            Undo.RecordObject(spline, "Remove Last Anchor");
            spline.RemoveLastAnchor();
            spline.SetDirty();
            serializedObject.Update();
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("dots"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("normal"));
    }

    public void OnSceneGUI()
    {
        SplineDone spline = (SplineDone)target;

        Vector3 transformPosition = spline.transform.position;

        List<SplineDone.Anchor> anchorList = spline.GetAnchorList();
        if (anchorList != null) 
        {
            foreach (SplineDone.Anchor anchor in spline.GetAnchorList())
            {
                Handles.color = Color.white;
                Handles.DrawWireCube(transformPosition + anchor.position, Vector3.one * .5f);

                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Handles.PositionHandle(transformPosition + anchor.position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(spline, "Change Anchor Position");
                    anchor.position = newPosition - transformPosition;
                    spline.SetDirty();
                    serializedObject.Update();
                }

                Handles.color = Color.green;
                Handles.SphereHandleCap(0, transformPosition + anchor.handleAPosition, Quaternion.identity, .5f, EventType.Repaint);

                EditorGUI.BeginChangeCheck();
                newPosition = Handles.PositionHandle(transformPosition + anchor.handleAPosition, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(spline, "Change Anchor Handle A Position");
                    anchor.handleAPosition = newPosition - transformPosition;
                    spline.SetDirty();
                    serializedObject.Update();
                }

                Handles.color = Color.blue;
                Handles.SphereHandleCap(0, transformPosition + anchor.handleBPosition, Quaternion.identity, .5f, EventType.Repaint);

                EditorGUI.BeginChangeCheck();
                newPosition = Handles.PositionHandle(transformPosition + anchor.handleBPosition, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(spline, "Change Anchor Handle B Position");
                    anchor.handleBPosition = newPosition - transformPosition;
                    spline.SetDirty();
                    serializedObject.Update();
                }

                Handles.color = Color.white;
                Handles.DrawLine(transformPosition + anchor.position, transformPosition + anchor.handleAPosition);
                Handles.DrawLine(transformPosition + anchor.position, transformPosition + anchor.handleBPosition);
            }

            // Draw Bezier
            for (int i = 0; i < spline.GetAnchorList().Count - 1; i++)
            {
                SplineDone.Anchor anchor = spline.GetAnchorList()[i];
                SplineDone.Anchor nextAnchor = spline.GetAnchorList()[i + 1];
                Handles.DrawBezier(transformPosition + anchor.position, transformPosition + nextAnchor.position, transformPosition + anchor.handleBPosition, transformPosition + nextAnchor.handleAPosition, Color.grey, null, 3f);
            }

            if(true)
            {
                // Spline is Closed Loop
                SplineDone.Anchor anchor = spline.GetAnchorList()[spline.GetAnchorList().Count - 1];
                SplineDone.Anchor nextAnchor = spline.GetAnchorList()[0];
                Handles.DrawBezier(transformPosition + anchor.position, transformPosition + nextAnchor.position, transformPosition + anchor.handleBPosition, transformPosition + nextAnchor.handleAPosition, Color.grey, null, 3f);

            }
        }
    }
}
