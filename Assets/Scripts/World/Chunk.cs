﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Chunk
{
    protected int _bottomY;
    protected int _topY;

    protected int _size;
    protected int _area;

    protected ArrayList _blockCoords;
    protected int _subChunkHeight;
    
    public int TopY{get {return _topY;}}

    public Chunk(int bottomY, int size, int area)
    {
        _bottomY = bottomY;
        _size = size;
        _area = area;

        _blockCoords = new ArrayList();
        _topY = _bottomY;
    }

    public abstract void Generate();

    public void InitTiles(TileBase tile){
        Generate();
        foreach(Vector3Int coord in _blockCoords){
            WorldGenerator.WG.BlocksTilemap.SetTile(coord, tile);
        }
    }

    public void Destroy(){
        foreach(Vector3Int coord in _blockCoords){
            WorldGenerator.WG.BlocksTilemap.SetTile(coord, null);
        }
    }

    /// <summary>
    /// Generate a straight line
    /// The y position is relative, so y1 = 0 or y2 = 0 always refer to the bottom of this chunk.
    ///
    /// RP = relative positon
    /// </summary>
    protected void GenerateLineAtRP(int x1, int y1, int x2, int y2){
        y1 += _bottomY;
        y2 += _bottomY;

        int diffX = x2 - x1;
        int diffY = y2 - y1;

        int signX = (int) Mathf.Sign(diffX);
        int signY = (int) Mathf.Sign(diffY);

        if(diffX != 0){
            for(int i = x1; i != x2; i += signX){
                _blockCoords.Add(new Vector3Int(i, y1, 0));
            }
        }else if(diffY != 0){
            for(int i = y1; i != y2; i += signY){
                _blockCoords.Add(new Vector3Int(x1, i, 0));
            }
        }
        
        _blockCoords.Add(new Vector3Int(x2, y2, 0));
    }

    /// <summary>
    /// Generate a copy of an prototype at a given grid position.
    /// Like `GenerateWall` this function uses relative y values, so y = 0 always is the bottom of this chunk.
    /// 
    /// RGP = relative grid position
    /// </summary>
    protected GameObject InstantiateAtRGP(GameObject prefab, float x, float y){
        return GameObject.Instantiate(
            prefab,
            new Vector3(
                x * WorldGenerator.WG.CellSize.x + WorldGenerator.WG.CellSize.x / 2,
                y * WorldGenerator.WG.CellSize.y + WorldGenerator.WG.CellSize.y / 2 + _bottomY * WorldGenerator.WG.CellSize.y,
                0
            ), Quaternion.identity
        );
    }

    protected void GenerateWallsAtRP(int height){
        GenerateLineAtRP(
            WorldGenerator.WG.LeftBound, 0,
            WorldGenerator.WG.LeftBound, height - 1
        );
        GenerateLineAtRP(
            WorldGenerator.WG.RightBound, 0,
            WorldGenerator.WG.RightBound, height - 1
        );
    }

    protected bool XPositionInTiles(int x, ArrayList tiles){
        foreach(Vector3Int tilePos in tiles){
            if(tilePos.x == x){
                return true;
            }
        }
        return false;
    }

    protected ArrayList GetTilesAtRGLine(int y){
        ArrayList tiles = new ArrayList();

        foreach(Vector3Int tilePos in _blockCoords){
            if(tilePos.y == y + _bottomY){
                tiles.Add(tilePos);
            }
        }

        return tiles;
    }

    protected int FindFreeXAtRGP(int y){
        int xGridPos = Random.Range(WorldGenerator.WG.LeftBound+1, WorldGenerator.WG.RightBound);
        int tries = 0;

        ArrayList tilesAtY = GetTilesAtRGLine(y);
        while(XPositionInTiles(xGridPos, tilesAtY)){
            xGridPos = Random.Range(WorldGenerator.WG.LeftBound+1, WorldGenerator.WG.RightBound);
            tries += 1;

            if(tries > 100){
                return -100;
            }
        }

        return xGridPos;
    }

    protected GameObject InstantiateMovingEnemyXAtRGP(int y){
        int xGridPos = FindFreeXAtRGP(y);
        GameObject enemy = InstantiateEnemyAtPosition(WorldGenerator.WG.MovingEnemyX, xGridPos, y);
        
        if(enemy != null){
            MoveHorizontally mh = enemy.GetComponent<MoveHorizontally>();
            mh.Speed = Random.Range(7.0f, 15.0f);
        }
        
        return enemy;
    }

    protected GameObject InstantiateStaticEnemyAtRGP(int x, int y){
        return InstantiateEnemyAtPosition(WorldGenerator.WG.StaticEnemy, x, y);
    }

    protected GameObject InstantiateFollowingEnemyAtRandomRGP(int y){
        int xGridPos = FindFreeXAtRGP(y);
        return InstantiateEnemyAtPosition(WorldGenerator.WG.FollowingEnemy, xGridPos, y);
    }

    protected GameObject InstantiateStaticEnemyAtRandomRGP(int y){
        int xGridPos = FindFreeXAtRGP(y);
        return InstantiateEnemyAtPosition(WorldGenerator.WG.StaticEnemy, xGridPos, y);
    }

    protected GameObject InstantiateEnemyAtPosition(GameObject enemyPrefab, int x, int y){
        float randomValue = Random.Range(0.0f, 1.0f);
        GameObject enemy = null;

        if(randomValue < GameManager.GM.GetRandomValue()){
            enemy = InstantiateAtRGP(enemyPrefab, x, y);
        }

        return enemy;
    }

}
