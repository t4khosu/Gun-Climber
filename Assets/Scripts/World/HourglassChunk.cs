using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourglassChunk : TriangleChunk
{

    public HourglassChunk(int positionY, int size, int area) : base(positionY, size, area){
        _left = WorldGenerator.WG.LeftBound;
        _right = WorldGenerator.WG.RightBound;
    }

    public override void Generate(){

        for(int i = 0; i < _size; i++){
            int yPos = _bottomY + 2 + i * (_height + 1);

            GenerateTriangle(
                height: _height,
                startPosY: yPos,
                narrow: 1,
                xPos: _left
            );

            GenerateTriangle(
                height: _height,
                startPosY: yPos,
                narrow: -1,
                xPos: _right
            );

            InstantiateStaticEnemyAtRGP(
                _left + _height / 2 + 2,
                _bottomY + _height / 2 + 2
            );

            InstantiateStaticEnemyAtRGP(
                _right - _height / 2 - 2,
                _bottomY + _height / 2 + 2
            );

            _topY = yPos + _height + 1;
        }



        GenerateWallsAtRP(height: _topY);
    }
}
