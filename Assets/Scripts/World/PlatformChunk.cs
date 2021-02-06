using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChunk : Chunk
{
    protected int _height = 10;
    protected int _left = 0;
    protected int _right = 0;
    protected GameObject _platformPrefab;
    protected float _gridY;

    public PlatformChunk(int positionY, int size, int area, GameObject platformPrefab, float gridY) : base(positionY, size, area){
        _platformPrefab = platformPrefab;
        _gridY = gridY;
    }

    public override void Generate(){
        GameObject.Instantiate(_platformPrefab, new Vector3(-6 * _gridY, (_top + 3) * _gridY, 0), Quaternion.identity);
        GameObject.Instantiate(_platformPrefab, new Vector3(-3 * _gridY, (_top + 8) * _gridY, 0), Quaternion.identity);

        _top += 10;
        GenerateWalls(height: _top);
    }
}
