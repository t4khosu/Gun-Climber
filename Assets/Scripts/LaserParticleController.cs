using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserParticleController : MonoBehaviour
{
    private float _speed = 9.0f;
    private Rigidbody2D _rb2d;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        _rb2d.velocity = Vector2.down * _speed;
    }
}
