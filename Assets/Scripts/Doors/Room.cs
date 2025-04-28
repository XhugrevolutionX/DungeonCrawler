using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    private DoorManager _doorManager;

    private Tilemap _walls;
    
    private List<Door> _doors = new List<Door>();
    
    private BoxCollider2D _col;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _doorManager = GetComponentInParent<DoorManager>();
        
        _walls = GetComponentInChildren<Grid>().transform.Find("Walls").GetComponent<Tilemap>();

        Bounds roomBounds = new Bounds(_walls.cellBounds.center + transform.position, _walls.cellBounds.size);

        foreach (var door in _doorManager.Doors)
        {
            if (roomBounds.Contains(door.transform.position))
            {
                _doors.Add(door.GetComponent<Door>());
            }
        }

        _col = transform.AddComponent<BoxCollider2D>();
        
        _col.offset = _walls.cellBounds.center;
        _col.size = roomBounds.size - new Vector3(2, 2);
        _col.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
    }


    void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(_walls.cellBounds.center + transform.position, _walls.cellBounds.size);
    }
    
}
