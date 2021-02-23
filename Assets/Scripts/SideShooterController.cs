using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideShooterController : MonoBehaviour
{
    [SerializeField] private GameObject _layerParticle;
    private string _position = "";
    private float _halfWidth = 0.0f;
    
    private const float MAX_TIMER = 0.7f;
    private float _time = 0.0f;

    public string Position{
        set{_position = value;}
    }

    // Start is called before the first frame update
    void Start()
    {
        _halfWidth = _layerParticle.GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if(_time >= MAX_TIMER){
            _time = 0.0f;
            SpawnLaserParticle();
        }
    }

    private void SpawnLaserParticle(){
        float cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        float x = 0f;

        if(_position == "left"){
            x = WorldGenerator.WG.LeftBound * WorldGenerator.WG.CellSize.x - 3 * WorldGenerator.WG.CellSize.x;
        }else{
            x = WorldGenerator.WG.RightBound * WorldGenerator.WG.CellSize.x + WorldGenerator.WG.CellSize.x + _halfWidth;
        }

        float addX = Random.Range(0, 3) * WorldGenerator.WG.CellSize.x + _halfWidth;

        Instantiate(
            _layerParticle,
            new Vector3(x + addX, cameraTop + 5, 0),
            Quaternion.identity
        );
    }
}
