﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{

    public static WorldGenerator WG;

    [SerializeField] private int _leftBound;
    [SerializeField] private int _rightBound;

    [SerializeField] private GameObject _grid;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _platform;

    [SerializeField] private TileBase _blockTile;
    [SerializeField] private GameObject _blade;

    private Tilemap _blocksTilemap;
    private Vector3 _cellSize;
    private int _topY;
    private float _cameraTop;

    public GameObject Blade{
        get {return _blade;}
    }

    public Vector2 CellSize{
        get {return _cellSize;}
    }

    public int LeftBound{
        get {return _leftBound;}
    }

    public int RightBound{
        get {return _rightBound;}
    }

    public Tilemap BlocksTilemap{
        get {return _blocksTilemap;}
    }

    void Awake()
    {
        if(WG != null && WG != this){
            Destroy(this.gameObject);
        }else{
            WG = this;
        }

        _blocksTilemap = _grid.transform.Find("BlockTilemap").gameObject.GetComponent<Tilemap>();
        _cellSize = _grid.GetComponent<Grid>().cellSize;
        _topY = 0;
        
        GenerateGround();
    }

    void Update()
    {
        while(NeedNewChunk()){
            AddRandomChunk();
        }
    }

    private bool NeedNewChunk(){
        _cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        return _cameraTop > _topY * _cellSize.y;
    }

    private void AddRandomChunk(){
        Chunk chunk = GetRandomChunk();
        chunk.InitTiles(_blockTile);
        _topY = chunk.Top;
    }

    private Chunk GetRandomChunk(){
        int randomChunk = Random.Range(0, 3);
        // int randomChunk = 1;

        if(randomChunk == 0){
            return new HourglassChunk(positionY: _topY, size: 1, area: 0);
        }else if (randomChunk == 1){
            return new TriangleChunk(positionY: _topY, size: 2, area: 0);
        }else{
            return new PlatformChunk(positionY: _topY, size: 1, area: 0, platformPrefab: _platform, gridY: _cellSize.y);
        }
    }

    void GenerateGround(){
        for(int i = _leftBound; i <= _rightBound; i+= 1){
            Vector3Int blockPos = new Vector3Int(i, 0, 0);
            WG.BlocksTilemap.SetTile(blockPos, _blockTile);
        }
    }
}
