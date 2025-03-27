using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class EnemyBehaviors : MonoBehaviour
{
    
    private Tilemap _groundTilemap;
    [SerializeField] private float randomPointRange = 5;
    private Transform _target;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _target = GetComponentInParent<EnemyManager>().Player.transform;
        _groundTilemap = GetComponentInParent<EnemyManager>().GroundTilemap;
    }
    
    public Vector3 Chase()
    {
        return _target.position;
    }

    public Vector3 Flee()
    {
        return transform.position - _target.position;
    }

    public Vector3 RandomPosition()
    {
        Vector3 position;
        do
        { 
            position = transform.position + new Vector3(Random.Range(-randomPointRange, randomPointRange), Random.Range(-randomPointRange, randomPointRange), 0);
        } while (!_groundTilemap.HasTile(_groundTilemap.WorldToCell(position)));
        
        return position;
    }
}
