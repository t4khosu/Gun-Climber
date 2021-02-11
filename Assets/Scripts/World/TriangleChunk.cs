using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleChunk : Chunk
{

    public TriangleChunk(int positionY, int size, int area) : base(positionY, size, area){
        _subChunkHeight = 9;
    }

    public override void Generate(){

        int dir = 1;
        for(int i = 0; i < _size; i++){
            
            int y = 2 + i * _subChunkHeight;

            if(dir == 1){
                GenerateRightTriangleRGP(
                    y: y,
                    height: _subChunkHeight - 2,
                    stretch: 2
                );
                InstantiateStaticEnemyAtRGP(
                    WorldGenerator.WG.RightBound - _subChunkHeight - 1,
                    y + _subChunkHeight / 2 - 1
                );
                
            }else{
                GenerateLeftTriangleRGP(
                    y: y,
                    height: _subChunkHeight - 2,
                    stretch: 2
                );
                InstantiateStaticEnemyAtRGP(
                    WorldGenerator.WG.LeftBound + _subChunkHeight + 1,
                    y + _subChunkHeight / 2 - 1
                );
            }
            
            InstantiateMovingEnemyXAtRGP(y);

            _topY += _subChunkHeight;
            dir *= -1;
        }

        GenerateWallsAtRP(height: _subChunkHeight * _size);
    }

    private void GenerateTriangleRGP(int x, int y, int height, int dir){
        int stretch = Mathf.Abs(dir);
        dir = (int) Mathf.Sign(dir);

        while(height >= 0){
            for(int i = 0; i < stretch; i += 1){
                GenerateLineAtRP(x, y, x, y + height - 1);
                x += dir;
            }
            
            y += 1;
            height -= 2;
        }
    }

    protected void GenerateLeftTriangleRGP(int y, int height, int stretch){
        GenerateTriangleRGP(WorldGenerator.WG.LeftBound+1, y, height, stretch);
    }

    protected void GenerateRightTriangleRGP(int y, int height, int stretch){
        GenerateTriangleRGP(WorldGenerator.WG.RightBound-1, y, height, -stretch);
    }
}
