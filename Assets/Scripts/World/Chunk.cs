using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Chunk
{
    protected int _positionY;
    protected int _size;
    protected int _area;

    protected ArrayList _blockCoords;
    protected int _top;

    public int Top{
        get {return _top;}
    }

    public Chunk(int positionY, int size, int area)
    {
        _positionY = positionY;
        _size = size;
        _area = area;

        _blockCoords = new ArrayList();
        _top = _positionY;
    }

    public abstract void Generate();

    public void InitTiles(TileBase tile){
        Generate();
        foreach(Vector3Int coord in _blockCoords){
            WorldGenerator.WG.BlocksTilemap.SetTile(coord, tile);
        }
        WorldGenerator.WG.BlocksTilemap.CompressBounds(); 
        WorldGenerator.WG.BlocksTilemap.RefreshAllTiles();
    }

    protected void GenerateLine(int x1, int y1, int x2, int y2){
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

    protected void GenerateWalls(int height){
        GenerateLine(
            WorldGenerator.WG.LeftBound, _positionY,
            WorldGenerator.WG.LeftBound, height - 1
        );
        GenerateLine(
            WorldGenerator.WG.RightBound, _positionY,
            WorldGenerator.WG.RightBound, height - 1
        );
        // GenerateLine(
        //     0, _positionY,
        //     0, height - 1
        // );
    }

    protected void AddBladeAt(int gridX, int gridY){
        float worldX = gridX * WorldGenerator.WG.CellSize.x + WorldGenerator.WG.CellSize.x / 2;
        float worldY = gridY * WorldGenerator.WG.CellSize.y + WorldGenerator.WG.CellSize.y / 2;

        GameObject.Instantiate(
            WorldGenerator.WG.Blade,
            new Vector3(worldX, worldY, 0),
            Quaternion.identity
        );
    }

}
