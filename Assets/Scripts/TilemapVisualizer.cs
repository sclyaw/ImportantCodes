
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class TilemapVisualizer : MonoBehaviour
{

    [SerializeField]
    private Tilemap floorTilemap;

    private TileBase floorTile;

    
    public void PaintFloorTiles(HashSet<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(HashSet<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var i in positions) {
            PaintSingleTile(tilemap, tile, i);      
        }

    }

    private void PaintSingleTile(Tilemap tilemap,TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition,tile);
    }
}
