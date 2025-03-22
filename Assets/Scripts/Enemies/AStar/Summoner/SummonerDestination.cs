using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class SummonerDestination : MonoBehaviour
{
    
    [SerializeField] private GameObject target;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private float movementRange = 5;
    private GameObject _targetDestination;
    public bool hasReachedDestination = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, _targetDestination.transform.position) < 0.2f)
        {
            Destroy(_targetDestination);
            hasReachedDestination = true;
        }
    }

    public Transform NewDestination()
    {
        Vector3 position;
        do
        { 
            position = transform.position + new Vector3(Random.Range(-movementRange, movementRange), Random.Range(-movementRange, movementRange), 0);
        } while (!groundTilemap.HasTile(groundTilemap.WorldToCell(position)));
        
        _targetDestination = Instantiate(target, position, Quaternion.identity);
        return _targetDestination.transform;
    }
}
