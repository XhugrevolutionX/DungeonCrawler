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
    
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _doorManager = GetComponentInParent<DoorManager>();
        
        _walls = GetComponentInChildren<Grid>().transform.Find("Walls").GetComponent<Tilemap>();
            
        Vector3 origin = _walls.cellBounds.position + transform.position;

        Bounds roomBounds = new Bounds(_walls.cellBounds.center + transform.position, _walls.cellBounds.size);

        foreach (var door in _doorManager.Doors)
        {
            if (roomBounds.Contains(door.transform.position))
            {
                _doors.Add(door.GetComponent<Door>());
            }
        }

        var col = transform.AddComponent<BoxCollider2D>();
        
        col.offset = new Vector2(0,0);
        col.size = roomBounds.size - new Vector3(2, 2);
        col.isTrigger = true;
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
