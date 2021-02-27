using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserParticleController : MonoBehaviour
{
    [SerializeField] private AudioClip _zapClip;
    private float _speed = 9.0f;
    private Rigidbody2D _rb2d;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        SoundManager.instance.PlaySound(_zapClip);
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
