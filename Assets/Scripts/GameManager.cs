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

    private bool _cameraStarted = false;
    private float _cameraSpeed = 5.0f;
    private float _maxCameraSpeed = 15.0f;
    private float _cameraSpeedIncreasePerSec = 0.1f;

    private bool _fixedCamera = false;

    public PlayerController PlayerControllerObject{
        get{return _playerController;}
    }

    public float CameraSpeed{
        set{_cameraSpeed = value;}
        get{return _cameraSpeed;}
    }

    public bool FixedCamera{
        set {_fixedCamera = value;}
        get {return _fixedCamera;}
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
        SetPlayerMaxHeight();
        MoveCamera();
    }

    private void SetPlayerMaxHeight(){
        float newHeight = _playerController.transform.position.y - _playerStartY;
        _maxHeight = Mathf.Max(_maxHeight, newHeight);
    }

    private void MoveCamera(){
        if(!_cameraStarted){
            return;
        }
        
        Camera.main.transform.Translate(Vector3.up * Time.deltaTime * _cameraSpeed);

        if(_cameraSpeed < _maxCameraSpeed){
            _cameraSpeed = Mathf.Min(_maxCameraSpeed, _cameraSpeed + _cameraSpeedIncreasePerSec * Time.deltaTime);
        }

    }

    public void IncreaseDestroyedBlocksByOne(){
        _destroyedBlocks += 1;
    }

    public void ActivateCameraMovement(){
        _cameraStarted = !_fixedCamera;
    }

    public void DeactivateCameraMovement(){
        _cameraStarted = false;
    }
}
