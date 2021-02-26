using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Start(){
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update () 
    {
         Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
         float angle = AngleBetweenTwoPoints(transform.position, mouseOnScreen);
         transform.rotation =  Quaternion.Euler(new Vector3(0f ,0f ,angle));
    }
 
    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void Flip(float dir){
        if(dir == -1){
            _spriteRenderer.flipY = false;
        }else{
            _spriteRenderer.flipY = true;
        }
    }
}
