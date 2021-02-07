using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y + 10 < Camera.main.transform.position.y - Camera.main.orthographicSize / 2){
            Destroy(gameObject);
        }
    }
}
