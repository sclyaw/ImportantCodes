using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;


public class SimpleRandomWalkMapGeneration : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLenght = 10;
    [SerializeField]
    public bool startRandomlyEachIteration = true;

    [SerializeField]
    private TilemapVisualizer tilemapVisualizer;


    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorpositions = RunRandomWalk();
        //foreach (var position in floorpositions) { Debug.Log(position); }
        tilemapVisualizer.PaintFloorTiles(floorpositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++) {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, walkLenght);
            floorPositions.UnionWith(path);
            if (startRandomlyEachIteration) { currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count)); }

        }
        return floorPositions;
    }


} 
