using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    
    [SerializeField] private GameObject _player;

    private float _maxHeight = 0.0f;
    private int _destroyedBlocks = 0;
    private PlayerController _playerController;
    private float _playerStartY = 0.0f;

    private float _activeCameraSpeed = 0.0f;
    private float _cameraSpeed = 5.0f;

    public PlayerController PlayerControllerObject{
        get{return _playerController;}
    }

    public float CameraSpeed{
        set{_cameraSpeed = value;}
        get{return _cameraSpeed;}
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

    public void IncreaseDestroyedBlocksByOne(){
        _destroyedBlocks += 1;
    }

    public void ActivateCameraMovement(){
        _activeCameraSpeed = _cameraSpeed;
    }

    public void DeactivateCameraMovement(){
        _activeCameraSpeed = 0;
    }
}
