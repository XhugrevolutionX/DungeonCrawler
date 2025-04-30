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

    private Bounds _roomBounds;
    
    private bool _passed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _doorManager = GetComponentInParent<DoorManager>();
        
        _walls = GetComponentInChildren<Grid>().transform.Find("Walls").GetComponent<Tilemap>();

        _roomBounds = new Bounds(new Vector3(_walls.cellBounds.center.x + transform.position.x, _walls.cellBounds.center.y + transform.position.y, 0), _walls.cellBounds.size);
        
        foreach (var door in _doorManager.Doors)
        {
            if (_roomBounds.Contains(door.transform.position))
            {
                _doors.Add(door.GetComponent<Door>());
            }
        }

        if (gameObject.name.Contains("StartRoom") || gameObject.name.Contains("TreasureRoom") || gameObject.name.Contains("ShopRoom"))
        {
            
        }
        else
        {
            _col = transform.AddComponent<BoxCollider2D>();
            _col.offset = _walls.cellBounds.center;
            _col.size = _roomBounds.size - new Vector3(3, 3);
            _col.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_passed)
            {
                _doors.ForEach(door => door.Close());   
            }
        }
    }


    // void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireCube(_roomBounds.center, _roomBounds.size);
    //     
    //     foreach (var door in _doorManager.Doors)
    //         Gizmos.DrawWireCube(door.transform.position, new Vector3(0.5f, 0.5f, 0));
    // }
    
}
