using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontally : MonoBehaviour
{
    [SerializeField] private LayerMask _solidLayer;
    [SerializeField] private float _speed;

    private int _dir;

    private Rigidbody2D _rb2d;
    private float _width;

    public float Speed{
        set{_speed = value;}
    }


    void Start()
    {
        _dir = Random.Range(0, 2) * 2 - 1;
        _rb2d = GetComponent<Rigidbody2D>();
        _width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        CheckForDirectionChange();
        Move();
    }

    void CheckForDirectionChange(){
        Vector2 pointOnBorder = new Vector2(transform.position.x + (_width / 2) * _dir, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(pointOnBorder, Vector2.right * _dir, 1f, _solidLayer);

        if(hit.collider != null){
            _dir *= -1;
        }
    }

    void Move(){
        transform.Translate(Vector3.right * _dir * _speed * Time.deltaTime);
    }
}
