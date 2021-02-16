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

        PlayerController.Player.IsInvincible = EditorGUILayout.Toggle(
            "Invincible Player", PlayerController.Player.IsInvincible
        );

        GameManager.GM.FixedCamera = EditorGUILayout.Toggle(
            "Fixed Camera", GameManager.GM.FixedCamera
        );

        GameManager.GM.CameraSpeed = EditorGUILayout.Slider(
            GameManager.GM.CameraSpeed, 0.0f, 30f
        );
    }
}
