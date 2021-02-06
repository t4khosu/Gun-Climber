using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    void Update () 
    {
         Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
         float angle = AngleBetweenTwoPoints(transform.position, mouseOnScreen);
         transform.rotation =  Quaternion.Euler(new Vector3(0f ,0f ,angle));
    }
 
    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
