using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColliderHandler : MonoBehaviour
{

    private GolfBall ball;
    private GridLayout grid;
    private Tilemap tileMap;

    private TileBase oldTile;

    public Action<GolfBall, string> OnTileEnter { get; set; }

    void Awake()
    {
        ball = FindObjectOfType<GolfBall>();
        tileMap = GetComponent<Tilemap>();
        grid = FindObjectOfType<GridLayout>();
    }

    // Update is called once per frame
    void Update()
    {
        var tilePosition = tileMap.WorldToCell(ball.Position);
        var newTile = tileMap.GetTile(tilePosition);
        if (newTile != null && oldTile != newTile)
        {
            OnTileEnter?.Invoke(ball, newTile.name);
        }
        oldTile = newTile;
    }
}
