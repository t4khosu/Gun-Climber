using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourglassChunk : TriangleChunk
{

    public HourglassChunk(int positionY, int size, int area) : base(positionY, size, area){
        _subChunkHeight = 9;
    }

    public override void Generate(){

        for(int i = 0; i < _size; i++){
            GenerateLeftTriangleRGP(
                y: 2,
                height: _subChunkHeight - 2,
                stretch: 1
            );

            GenerateRightTriangleRGP(
                y: 2,
                height: _subChunkHeight - 2,
                stretch: 1
            );

            InstantiateStaticEnemyAtRGP(
                WorldGenerator.WG.LeftBound + _subChunkHeight / 2 + 1,
                _subChunkHeight / 2 + 1
            );

            InstantiateStaticEnemyAtRGP(
                WorldGenerator.WG.RightBound - _subChunkHeight / 2 - 1,
                _subChunkHeight / 2 + 1
            );

            InstantiateMovingEnemyXAtRGP(7);

            _topY += _subChunkHeight;
        }

        GenerateWallsAtRP(height: _subChunkHeight * _size);
    }
}
