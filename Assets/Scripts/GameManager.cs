﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    
    [HideInInspector] public float _maxHeight = 0.0f;
    [SerializeField] private GameObject _player;
    private PlayerController _playerController;
    private float _playerStartY = 0.0f;

    private float _activeCameraSpeed = 0.0f;
    private float _cameraSpeed = 5.0f;

    public PlayerController PlayerControllerObject{
        get{return _playerController;}
    }

    void Awake()
    {
        if(GM == null){
            GM = this;
        }

        _playerController = _player.GetComponent<PlayerController>();
        _playerStartY = _playerController.transform.position.y;
    }

    void Update(){
        float newHeight = _playerController.transform.position.y - _playerStartY;
        _maxHeight = Mathf.Max(_maxHeight, newHeight);

        Camera.main.transform.Translate(Vector3.up * Time.deltaTime * _activeCameraSpeed);
    }

    public static bool containsLayerMask(LayerMask objectLayer, LayerMask layers){
        return (layers & 1 << objectLayer) == 1 << objectLayer;
    }

    public void ActivateCameraMovement(){
        _activeCameraSpeed = _cameraSpeed;
    }

    public void DeactivateCameraMovement(){
        _activeCameraSpeed = 0;
    }
}
