using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GameManager gameManagerScript = (GameManager) target;

        if(GUILayout.Button("Activate Camera"))
        {
            gameManagerScript.ActivateCameraMovement();
        }

        if(GUILayout.Button("Deactivate Camera"))
        {
            gameManagerScript.DeactivateCameraMovement();
        }
    }
}
