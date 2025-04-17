using ProceduralLevelGenerator.Unity.Editor.LevelGenerators;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomTags : MonoBehaviour
{
    private GameObject _tilemaps;
    private GameObject _floors;
    private GameObject _walls;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        ApplyTags();
    }
    
    private void ApplyTags()
    {
        _tilemaps = transform.GetChild(1).gameObject;
        _floors = _tilemaps.transform.GetChild(0).gameObject;
        _walls = _tilemaps.transform.GetChild(1).gameObject;
        
        _floors.tag = "Ground";
        _walls.tag = "Walls";
    }
}
