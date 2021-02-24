using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Player;

    [SerializeField] private LayerMask _solidLayerMask;
    [SerializeField] private float _onGroundMoveSpeed;
    [SerializeField] private float _inAirMoveSpeed;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _shootForce;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    
    private float _moveDir = 0;
    private float _lookDir = 0;
    private bool _jump = false;
    private bool _shoot = false;
    private bool _isInvincible = false;

    private float _spriteWidth;

    private Rigidbody2D _rb2d;
    private BoxCollider2D _boxCollider;
    private Animator _animator;
    

    public Vector3 Velocity{
        get{return _rb2d.velocity;}
    }

    public bool IsInvincible{
        set{_isInvincible=value;}
        get{return _isInvincible;}
    }

    void Awake(){
        if(Player != null && Player != this){
            Destroy(this.gameObject);
        }else{
            Player = this;
        }
    }

    void Start()
    {
        _rb2d = transform.GetComponent<Rigidbody2D>();
        _boxCollider = transform.GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        _moveDir = Input.GetAxis("Horizontal");
        _jump = Input.GetAxis("Vertical") > 0;
        _shoot = _shoot || Input.GetMouseButtonDown(0);

        if(_moveDir != 0){
            _lookDir = _moveDir < 0 ? -1 : 1;
        }

        _animator.SetFloat("Look Direction", _lookDir);
        _animator.SetFloat("Move Speed", Mathf.Abs(_moveDir));
    }

    void FixedUpdate(){
        if(IsGrounded()){
            CheckMoveXOnGround();
            CheckJump();
            _animator.SetBool("In Air", false);
        }else{
            CheckMoveXInAir();
            _animator.SetBool("In Air", true);
        }

        if(_shoot){
            Shoot();
            _shoot = false;
        }

        if(AtCameraEdge()){
            _rb2d.velocity *= Vector2.up;
        }
    }

    private bool AtCameraEdge(){
        return (transform.position.x <= GameManager.GM.CameraLeftBound + _spriteWidth
        ) || (transform.position.x >= GameManager.GM.CameraRightBound - _spriteWidth);
    }

    private void Shoot(){
        GameManager.GM.TryToStartGame();
        
        Vector2 direction = (Vector2) (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        direction.Normalize();

        _rb2d.velocity -= direction * this._shootForce;

        Launch(direction);
    }

    private void CheckMoveXOnGround(){
        _rb2d.velocity = new Vector2(_moveDir * this._onGroundMoveSpeed, _rb2d.velocity.y);
    }

    private void CheckMoveXInAir(){
        float forceX = _moveDir * _inAirMoveSpeed;

        if(_moveDir > 0){
            float possibleAddForce = this._inAirMoveSpeed - this._rb2d.velocity.x;
            float addForceX = Mathf.Min(this._inAirMoveSpeed, possibleAddForce);
            this._rb2d.AddForce(Vector2.right * addForceX);
        }else if(_moveDir < 0){
            float possibleAddForce = this._inAirMoveSpeed - Mathf.Abs(this._rb2d.velocity.x);
            float addForceX = Mathf.Min(this._inAirMoveSpeed, possibleAddForce);
            this._rb2d.AddForce(-1 * Vector2.right * addForceX);
        }
    }

    private void Launch(Vector2 direction){
        GameObject bullet = Instantiate(
            _bulletPrefab,
            _rb2d.position,
            Quaternion.identity
        );

        BulletController bc = bullet.GetComponent<BulletController>();
        bc.Speed = _bulletSpeed;
        bc.Direction = direction;
    }

    private bool IsGrounded(){
        Vector2 size = new Vector2(_boxCollider.bounds.size.x * 0.9f, _boxCollider.bounds.size.y);
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            _boxCollider.bounds.center, size, 0f, Vector2.down, .02f, _solidLayerMask
        );
        
        return raycastHit.collider != null;
    }

    private void CheckJump(){
        if(this._jump){
            this._rb2d.velocity = new Vector2(this._rb2d.velocity.x, this._jumpVelocity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(_isInvincible){
            return;
        }

        if(other.tag == "Death"){
            gameObject.SetActive(false);
            GameManager.GM.Failed();
        }
    }
}
