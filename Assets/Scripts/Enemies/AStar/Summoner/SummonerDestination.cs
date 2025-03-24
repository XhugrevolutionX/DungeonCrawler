using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SummonerDestination : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject target;
    private EnemiesDamage _enemiesDamage;
    private Tilemap _groundTilemap;
    [SerializeField] private float movementRange = 5;
    private GameObject _targetDestination;
    public bool hasReachedDestination = false;

    private void Start()
    {
        _groundTilemap = GetComponentInParent<EnemyManager>().GroundTilemap;
        _enemiesDamage = GetComponentInChildren<EnemiesDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetDestination != null)
        {
            if (_enemiesDamage.isDead)
            {
                Destroy(_targetDestination);
            }
            if (Vector2.Distance(transform.position, _targetDestination.transform.position) < 0.5f)
            {
                animator.ResetTrigger("Damaged");
                Destroy(_targetDestination);
                hasReachedDestination = true;
            }
        }
    }

    public Transform NewDestination()
    {
        Vector3 position;
        do
        { 
            position = transform.position + new Vector3(Random.Range(-movementRange, movementRange), Random.Range(-movementRange, movementRange), 0);
        } while (!_groundTilemap.HasTile(_groundTilemap.WorldToCell(position)));
        
        _targetDestination = Instantiate(target, position, Quaternion.identity);
        return _targetDestination.transform;
    }
}
