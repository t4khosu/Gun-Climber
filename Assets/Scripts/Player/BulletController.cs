using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float maxSecAlive;
    [SerializeField] private LayerMask _solidLayer;
    
    private float _timeAlive;
    private float _speed;

    private Rigidbody2D _rb2d;
    private Vector2 _dir;
    

    public Vector2 Direction{
        set {_dir = value;}
    }

    public float Speed{
        set {_speed = value;}
    }


    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _timeAlive = 0;
    }

    void Update()
    {
        if(_timeAlive > maxSecAlive){
            Destroy(gameObject);
        }
        _timeAlive += Time.deltaTime;
    }

    void FixedUpdate()
    {
        _rb2d.velocity = _dir * _speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Blocks")){
            Vector2 contactNormal = other.contacts[0].normal;
            contactNormal.x = Mathf.Abs(contactNormal.x * 0.5f) * Mathf.Sign(this._dir.x);
            contactNormal.y = Mathf.Abs(contactNormal.y * 0.5f) * Mathf.Sign(this._dir.y);
            Vector2 tilePosition = contactNormal + other.contacts[0].point;

            WorldGenerator.WG.BlocksTilemap.SetTile(
                WorldGenerator.WG.BlocksTilemap.WorldToCell(tilePosition), 
                null
            );
            Destroy(gameObject); 
        }else if(other.gameObject.layer == LayerMask.NameToLayer("Platform")){
            Destroy(gameObject); 
        }
        
    }

}
