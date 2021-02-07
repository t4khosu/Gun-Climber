using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontally : MonoBehaviour
{
    [SerializeField] private LayerMask _solidLayer;
    [SerializeField] private float _speed;
    [SerializeField] private bool _addSpacingBeforeCollision;

    private int _dir;

    private Rigidbody2D _rb2d;
    private float _width;
    private float _leftBoundPixelPos;
    private float _rightBoundPixelPos;

    public float Speed{
        set{_speed = value;}
    }


    void Start()
    {
        _dir = Random.Range(0, 2) * 2 - 1;
        _rb2d = GetComponent<Rigidbody2D>();
        _width = GetComponent<SpriteRenderer>().bounds.size.x;

        _leftBoundPixelPos = WorldGenerator.WG.LeftBound * WorldGenerator.WG.CellSize.x + WorldGenerator.WG.CellSize.x + _width / 2;
        _rightBoundPixelPos = WorldGenerator.WG.RightBound * WorldGenerator.WG.CellSize.x - _width / 2;

        if(_addSpacingBeforeCollision){
            _leftBoundPixelPos += 0.8f * WorldGenerator.WG.CellSize.x;
            _rightBoundPixelPos -= 0.8f * WorldGenerator.WG.CellSize.x;
        }
    }

    void FixedUpdate()
    {
        CheckForDirectionChange();
        Move();
    }

    void CheckForDirectionChange(){
        if (WillCollideWithBlock() || WillLeaveSideBounds()){
            _dir *= -1;
        }
    }

    bool WillCollideWithBlock(){
        Vector2 pointOnBorder = new Vector2(transform.position.x + (_width / 2) * _dir, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(pointOnBorder, Vector2.right * _dir, 1f, _solidLayer);
        return hit.collider != null;
    }

    bool WillLeaveSideBounds(){
        return transform.position.x <= _leftBoundPixelPos || transform.position.x >= _rightBoundPixelPos;
    }

    void Move(){
        transform.Translate(Vector3.right * _dir * _speed * Time.deltaTime);
    }
}
