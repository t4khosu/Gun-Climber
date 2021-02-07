using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleChunk : Chunk
{
    protected int _height = 6;
    protected int _left = 0;
    protected int _right = 0;

    public TriangleChunk(int positionY, int size, int area) : base(positionY, size, area){
        _left = WorldGenerator.WG.LeftBound;
        _right = WorldGenerator.WG.RightBound;
    }

    public override void Generate(){

        int direction = 1;
        for(int i = 0; i < _size; i++){
            int xPos = i % 2 == 0 ? _left : _right;
            int yPos = _bottomY + 2 + i * (_height + 1);

            GenerateTriangle(
                height: _height,
                startPosY: yPos,
                narrow: direction * 2,
                xPos: xPos
            );

            InstantiateStaticEnemyAtRGP(
                xPos + direction * _height + direction * 2,
                yPos + _height / 2 + 2
            );


            _topY = yPos + _height + 1;
            direction *= -1;
        }

        GenerateWallsAtRP(height: _topY);
    }

    protected void GenerateTriangle(int height, int startPosY, int narrow, int xPos){
        int bottom = startPosY;
        int top = startPosY + height;
        int relX = 1;

        int direction = (int) Mathf.Sign(narrow);
        narrow = Mathf.Abs(narrow);

        while(bottom <= top){
            for(int i = 0; i < narrow; i += 1){
                int newX = xPos + direction * (relX + i);
                GenerateLineAtRP(newX, bottom, newX, top);
            }
            
            relX += narrow;
            bottom += 1;
            top -= 1;
        }
    }
}
