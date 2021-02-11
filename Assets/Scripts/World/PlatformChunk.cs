using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChunk : Chunk
{
    public PlatformChunk(int positionY, int size, int area) : base(positionY, size, area){
        _subChunkHeight = 10;
    }

    public override void Generate(){
        InstantiatePlatformAtRGP(-3, 3);

        InstantiateMovingEnemyXAtRGP(5);
        InstantiateFollowingEnemyAtRandomRGP(7);
        InstantiateStaticEnemyAtRandomRGP(10);

        InstantiatePlatformAtRGP(0, 8);

        _topY += _subChunkHeight;
        GenerateWallsAtRP(height: _subChunkHeight);
    }

    private GameObject InstantiatePlatformAtRGP(float x, float y){
        GameObject platform = InstantiateAtRGP(WorldGenerator.WG.Platform, x, y);
        MoveHorizontally mh = platform.GetComponent<MoveHorizontally>();
        mh.Speed = Random.Range(5.0f, 20.0f);
        return platform;
    }
}
