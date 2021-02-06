using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPlayerJump : MonoBehaviour
{
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    [SerializeField] private float _slowMultiplier = 3f;

    Rigidbody2D _rb2d;

    void Awake(){
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(_rb2d.velocity.y < 0){
            _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        } else if(_rb2d.velocity.y > 0 && !(Input.GetAxis("Vertical") > 0)){
            _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if(_rb2d.velocity.y != 0 && Mathf.Abs(Input.GetAxis("Horizontal")) != 1){
            if (_rb2d.velocity.x > 0){
                _rb2d.velocity -= Vector2.right * _slowMultiplier * Time.deltaTime;
            }
            if(_rb2d.velocity.x < 0){
                _rb2d.velocity += Vector2.right * _slowMultiplier * Time.deltaTime;
            }
        }
    }
}
