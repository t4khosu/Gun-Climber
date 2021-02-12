using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(GameManager))]
public class GameManagerEditor : Editor
{

    bool playerInvincibleButton = false;
    float cameraSpeed = 0.0f;

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GameManager gameManagerScript = (GameManager) target;

        PlayerController.Player.IsInvincible = EditorGUILayout.Toggle(
            "Invincible Player", PlayerController.Player.IsInvincible
        );

        GameManager.GM.CameraSpeed = EditorGUILayout.Slider(
            GameManager.GM.CameraSpeed, 0.0f, 30f
        );
    }
}
