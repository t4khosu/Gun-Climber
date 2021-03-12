using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _shooter;
    
    private bool _gameStarted = false;
    private bool _failed = false;

    private float _maxHeight = -1.0f;
    private int _destroyedBlocks = 0;
    private PlayerController _playerController;
    private float _playerStartY = 0.0f;

    private bool _cameraStarted = false;
    private float _cameraSpeed = 5.0f;
    private float _maxCameraSpeed = 15.0f;
    private float _cameraSpeedIncreasePerSec = 0.1f;

    private float _cameraLeftBound;
    private float _cameraRightBound;

    private float _blocksLeftBound;
    private float _blocksRightBound;

    private bool _fixedCamera = false;

    

    public PlayerController PlayerControllerObject{
        get{return _playerController;}
    }

    public float CameraSpeed{
        set{_cameraSpeed = value;}
        get{return _cameraSpeed;}
    }

    public float CameraLeftBound{
        get{return _cameraLeftBound;}
    }

    public float CameraRightBound{
        get{return _cameraRightBound;}
    }

    public bool FixedCamera{
        set {_fixedCamera = value;}
        get {return _fixedCamera;}
    }

    public void TryToStartGame(){
        if(!_gameStarted){
            _gameStarted = true;
            UIController.instance.Manual.text = "";
            ActivateCameraMovement();
        }
    }

    void Awake()
    {
        if(GM == null){
            GM = this;
        }

        float halfCameraWidth = Camera.main.aspect * Camera.main.orthographicSize;
        _cameraLeftBound = Camera.main.transform.position.x - halfCameraWidth;
        _cameraRightBound = _cameraLeftBound + 2 * halfCameraWidth;

        _playerController = _player.GetComponent<PlayerController>();
        _playerStartY = _playerController.transform.position.y;

        _shooter.SetActive(false);
    }

    void Start(){
        _blocksLeftBound = WorldGenerator.WG.LeftBound * WorldGenerator.WG.CellSize.x;
        _blocksRightBound = WorldGenerator.WG.RightBound * WorldGenerator.WG.CellSize.x + WorldGenerator.WG.CellSize.x;
    }

    void Update(){
        SetPlayerMaxHeight();
        MoveCamera();
        CheckSideShooter();

        if(_failed){
            bool pressedEnter = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);
            bool pressedSpace = Input.GetKeyDown("space");
            if(pressedEnter || pressedSpace){
                SceneManager.LoadScene("Game");
            }
        }
    }

    private void SetPlayerMaxHeight(){
        float newHeight = (int) (_playerController.transform.position.y - _playerStartY);

        if(newHeight > _maxHeight){
            UIController.instance.Score.text = string.Concat(newHeight, "m");
            _maxHeight = newHeight;
        }
        
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

    private void CheckSideShooter(){
        if(_playerController.transform.position.x <= _blocksLeftBound){
            if(!_shooter.activeSelf){
                _shooter.SetActive(true);
                _shooter.GetComponent<SideShooterController>().Position = "left";
            }
        } else if(_playerController.transform.position.x > _blocksRightBound){
            if(!_shooter.activeSelf){
                _shooter.SetActive(true);
                _shooter.GetComponent<SideShooterController>().Position = "right";
            }
        } else {
            _shooter.SetActive(false);
        }
    }

    public void Failed(){
        _failed = true;

        int highscore = SetAndGetHighscore();
        UIController.instance.FailedCanvas.SetActive(true);
        UIController.instance.Highscore.text = string.Concat("Highscore: ", highscore, "m");
        UIController.instance.FinalScore.text = string.Concat("Climbed Height: ", _maxHeight, "m");
        DeactivateCameraMovement();
    }

    private int SetAndGetHighscore(){
        int highscore = 0;
        if(PlayerPrefs.HasKey("highscore")){
            highscore = PlayerPrefs.GetInt("highscore");
        }
        highscore = (int) Mathf.Max(_maxHeight, highscore);
        PlayerPrefs.SetInt("highscore", highscore);
        PlayerPrefs.Save();

        return highscore;
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

    public float GetRandomValue(){
        return _maxHeight / 400;
    }
}
