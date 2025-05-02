using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> doors = new List<GameObject>();
    [SerializeField] private bool switchDoorsState = false;

    public List<GameObject> Doors => doors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (switchDoorsState)
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<Door>().switchState = true;
            }
            switchDoorsState = false;
        }
    }

    private void CloseDoors()
    {
        foreach (var door in doors)
        {
            door.GetComponent<Door>().Close();
        }
    }

    private void OpenDoors()
    {
        foreach (var door in doors)
        {
            door.GetComponent<Door>().Open();
        }
    }

    public void SetDoors(List<GameObject> _doors)
    {
        doors = _doors;
    }

    public bool HasDoor(Vector2 pos)
    {
        foreach (var door in doors)
        {
            if (door.GetComponent<Collider2D>().bounds.Contains(pos))
            {
                Debug.Log(door.name);
                return true;
            }
        }
        return false;
    }
}
