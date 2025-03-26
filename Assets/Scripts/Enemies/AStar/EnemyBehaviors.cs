using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class EnemyBehaviors : MonoBehaviour
{
    
    private Transform _target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _target = GetComponentInParent<EnemyManager>().Player.transform;
    }
    
    public Vector3 Chase()
    {
        return _target.position;
    }

    public Vector3 Flee()
    {
        return transform.position - _target.position;
    }
}
