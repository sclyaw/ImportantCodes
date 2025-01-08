using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralFloorGenerator : MonoBehaviour
{
    public Tilemap floorTilemap;              // Reference to the Tilemap component for floors
    public TileBase floorTile;                // Floor tile to be placed
    public int dungeonWidth = 10;             // Width of the dungeon
    public int dungeonHeight = 10;            // Height of the dungeon
    public int seed = 0;                      // Seed for random generation
    public bool randomizeSeed = true;

    private HashSet<Vector2Int> floorPositions;

    void Start()
    {
        if (randomizeSeed)
            seed = Random.Range(int.MinValue, int.MaxValue);

        Random.InitState(seed);
        floorPositions = GenerateFloorPositions();
        DrawFloor();
    }

    HashSet<Vector2Int> GenerateFloorPositions()
    {
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();

        Vector2Int currentPos = Vector2Int.zero;
        positions.Add(currentPos);

        for (int i = 0; i < dungeonWidth * dungeonHeight; i++)
        {
            Vector2Int newPos = currentPos + RandomDirection();
            if (newPos.x >= -dungeonWidth / 2 && newPos.x <= dungeonWidth / 2 &&
                newPos.y >= -dungeonHeight / 2 && newPos.y <= dungeonHeight / 2)
            {
                positions.Add(newPos);
                currentPos = newPos;
            }
        }

        return positions;
    }

    Vector2Int RandomDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0: return Vector2Int.up;
            case 1: return Vector2Int.down;
            case 2: return Vector2Int.left;
            case 3: return Vector2Int.right;
            default: return Vector2Int.zero;
        }
    }

    void DrawFloor()
    {
        foreach (Vector2Int position in floorPositions)
        {
            floorTilemap.SetTile((Vector3Int)position, floorTile);
        }
    }
}