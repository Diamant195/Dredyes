using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGeneration : MonoBehaviour
{
    public int width;
    public int height;
    [Range(0, 8)] public int threshold;
    public GameObject WorldTile;
    public GameObject WorldBorderTile;
    public int smoothening;
    private int[,] tiles;
    [Range(0, 100)] public int tileSpawnRate;
    private void Awake()
    {
        GenerateZone();
    }
    void Start()
    {
        PlaceTiles();
    }
    private void GenerateZone()
    {
        tiles = new int[width, height];
        int seed = Random.Range(0, 1000000);
        System.Random randChoice = new System.Random(seed.GetHashCode());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (randChoice.Next(0, 100) < tileSpawnRate)
                {
                    tiles[x, y] = 1;
                }
                else
                {
                    tiles[x, y] = 0;
                }
            }
        }
        for (int i = 0; i < smoothening; i++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int neighboringWalls = GetNeighbors(x, y);

                    if (neighboringWalls > threshold)
                    {
                        tiles[x, y] = 1;
                    }
                    else if (neighboringWalls < threshold)
                    {
                        tiles[x, y] = 0;
                    }
                }
            }
        }
        for (int x = 0; x < width; x++) for (int y = 0; y < height; y++) if (x == 0 || y == 0 || x == width - 1 || y == height - 1) tiles[x, y] = 2;
    }
    private int GetNeighbors(int pointX, int pointY)
    {
        int wallNeighbors = 0;
        for (int x = pointX - 1; x <= pointX + 1; x++)
        {
            for (int y = pointY - 1; y <= pointY + 1; y++)
            {
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    if (x != pointX || y != pointY)
                    {
                        if (tiles[x, y] == 1)
                        {
                            wallNeighbors++;
                        }
                    }
                }
                else
                {
                    wallNeighbors++;
                }
            }
        }
        return wallNeighbors;
    }
    private void PlaceTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y] == 1)
                {
                    Instantiate(WorldTile, new Vector2(x * 8.101448f, y * 8.101448f), Quaternion.identity, gameObject.transform);
                }
                else if(tiles[x, y] == 2)
                {
                    Instantiate(WorldBorderTile, new Vector2(x * 8.101448f, y * 8.101448f), Quaternion.identity, gameObject.transform);
                }
            }
        }
    }
}
