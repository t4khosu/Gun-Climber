using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChunk : Chunk
{
    protected int _subChunkHeight = 10;

    public PlatformChunk(int positionY, int size, int area) : base(positionY, size, area){
    }

    public override void Generate(){
        InstantiatePlatformAtRGP(
            x: -3, y: 3, speed: Random.Range(5.0f, 20.0f)
        );

        InstantiatePlatformAtRGP(
            x: 0, y: 8, speed: Random.Range(5.0f, 20.0f)
        );

        _topY += _subChunkHeight;
        GenerateWallsAtRP(height: _subChunkHeight);
    }

    private GameObject InstantiatePlatformAtRGP(float x, float y, float speed){
        GameObject platform = InstantiateAtRGP(WorldGenerator.WG.Platform, x, y);
        MoveHorizontally mh = platform.GetComponent<MoveHorizontally>();
        mh.Speed = speed;

        return platform;
    }
}
