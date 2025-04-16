using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class GeneratorBSP : MonoBehaviour
{
    [SerializeField] private int startHeight;
    [SerializeField] private int startWidth;
    [SerializeField] private Vector2Int dungeonCenter;
    
    [SerializeField] private Tilemap map;
    [SerializeField] private List<TileBase> tiles;
    [SerializeField] private TileBase startTile;
    [SerializeField] private TileBase endTile;

    [Header("Settings")] [SerializeField] private float maxSize = 20f;
    
    private BSPNode _bspRoot;

    private List<BoundsInt> _rooms = new List<BoundsInt>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bspRoot = new BSPNode(dungeonCenter.x, dungeonCenter.y, startWidth, startHeight, true);
        
        _bspRoot.Process(_rooms, maxSize);

        foreach (var room in _rooms)
        {
            BoundsInt drawRoom = room;
            drawRoom.size = room.size - new Vector3Int(2, 2, 0);
            
            FillTilemap(drawRoom);
        }

        List<BoundsInt> orderedRooms = _rooms.OrderBy(r => Vector3.Distance(new Vector3Int(dungeonCenter.x, dungeonCenter.y, 0), r.center)).ToList();
        
        BoundsInt _startRoom = orderedRooms.First();
        BoundsInt _endRoom = orderedRooms.Last();

        Vector3Int startPosition = new Vector3Int((int)_startRoom.center.x, (int)_startRoom.center.y, 0);
        Vector3Int endPosition = new Vector3Int((int)_endRoom.center.x, (int)_endRoom.center.y, 0);
        
        
        map.SetTile(startPosition, startTile);
        map.SetTile(endPosition, endTile);
        
        List<Vector3Int> mainCorridor = new List<Vector3Int>();
        Vector3Int corridorPosition = startPosition;

        Vector3Int corridorDirection = endPosition - startPosition;
        
        do
        {
            corridorPosition += corridorDirection.x >= 0 ? new Vector3Int(1, 0, 0) : new Vector3Int(-1, 0, 0);
            mainCorridor.Add(corridorPosition);
        } while (corridorPosition.x != endPosition.x);
        do
        {
            corridorPosition += corridorDirection.y >= 0 ? new Vector3Int(0, 1, 0) : new Vector3Int(0, -1, 0);
            mainCorridor.Add(corridorPosition);
            
        } while (corridorPosition.y != endPosition.y);

        foreach (var tilePos in mainCorridor)
        {
            if (map.HasTile(tilePos))
            {
                
            }
            else
            {
                map.SetTile(tilePos, GetRandomTile());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDrawGizmos()
    {
        if (_bspRoot != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_bspRoot.Room.center, _bspRoot.Room.size);
        }

        foreach (BoundsInt room in _rooms)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(room.center, room.size);
        }
    }

    private void FillTilemap(BoundsInt bounds)
    {
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
               map.SetTile(new Vector3Int(x, y, 0), GetRandomTile());
            }
        }
    }
    
    private void AddTile(Vector3Int position, ref int count)
    {
        if (map.HasTile(position))
            return;

        map.SetTile(position, GetRandomTile());
        count++;
    }
    private TileBase GetRandomTile()
    {
        return tiles[Random.Range(0, tiles.Count)];
    }
}
