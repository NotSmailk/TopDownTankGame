using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyDetection))]
public class EnemyDetectionEditor : Editor
{

#if UNITY_EDITOR

    private void OnSceneGUI()
    {
        EnemyDetection enemyDetection = (EnemyDetection)target;

        Vector3 targetPosition = enemyDetection.transform.position;

        #region Circles

        DrawSphere(Color.red, targetPosition, enemyDetection.DetectionSphereRadius);

        DrawSphere(Color.yellow, targetPosition, enemyDetection.RadiusAtObservation);

        DrawSphere(Color.green, targetPosition, enemyDetection.DetectionViewRadius);

        #endregion Circles
    }

#endif

    #region DrawMethods

    private void DrawSphere(Color color, Vector3 position, float radius)
    {
        Handles.color = color;

        Handles.DrawWireArc(position, Vector3.up, Vector3.right, 360, radius);
    }

    private void DrawLine(Color color, Vector3 position, Vector3 viewAngle, float radius)
    {
        Handles.color = color;

        Handles.DrawLine(position, position + viewAngle * radius);
    }

    #endregion DrawMethods
}
