using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowerController : MonoBehaviour
{
    private bool _isMoving = false;
    private Vector2 _dir;
    private Rigidbody2D _rb2d;
    private SpriteRenderer _sprite;

    private float _speed = 11f;
    private float _timeAlive = 0.0f;
    private float _maxSecAlive = 3.5f;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!_isMoving){
            CheckIfShouldStartMoving();
        }else{
            if(_timeAlive > _maxSecAlive){
                Destroy(gameObject);
            }
            _timeAlive += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if(_isMoving){
            _rb2d.velocity = _dir * _speed;
        }
    }

    private void CheckIfShouldStartMoving(){
        float yDistance = transform.position.y - PlayerController.Player.transform.position.y;
        float yVelocity = PlayerController.Player.Velocity.y;

        if(yDistance < 0 && yVelocity < 0){
            _isMoving = true;
            StartFollowing();
        }
    }

    private void StartFollowing(){
        _dir = (Vector2) (PlayerController.Player.transform.position - transform.position);
        _dir.Normalize();
        _sprite.color = new Color (1, 0, 0, 1); 
    }
}
