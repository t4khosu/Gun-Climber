using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChunk : Chunk
{
    protected int _subChunkHeight = 10;
    protected GameObject _platformPrefab;

    public PlatformChunk(int positionY, int size, int area, GameObject platformPrefab) : base(positionY, size, area){
        _platformPrefab = platformPrefab;
    }

    public override void Generate(){
        InstantiatePlatformAtRelativePosition(
            x: -3, y: 3, speed: Random.Range(5.0f, 20.0f)
        );

        InstantiatePlatformAtRelativePosition(
            x: 0, y: 8, speed: Random.Range(5.0f, 20.0f)
        );

        _top += _subChunkHeight;
        GenerateWalls(height: _top);
    }

    private GameObject InstantiatePlatformAtRelativePosition(float x, float y, float speed){
        GameObject platform = InstantiateAtGridPosition(_platformPrefab, x, _top + y);
        PlatformController pc = platform.GetComponent<PlatformController>();
        pc.Speed = speed;

        return platform;
    }
}
