using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    
    [SerializeField] private float chasingDistanceThreshold;
    [SerializeField] private float fleeingDistanceThreshold;

    private EnemiesDamage _enemiesDamage;
    private float _distanceToTarget;
    private Transform _target;
    private bool _fleeComplete;
    private bool _chaseComplete;

    public bool ChaseComplete => _chaseComplete;
    public bool FleeComplete => _fleeComplete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _enemiesDamage = GetComponentInChildren<EnemiesDamage>();
        _target = GetComponentInParent<EnemyManager>().Player.transform;
    }

    void Update()
    {
        StateSensor();
    }

    private void StateSensor()
    {
        if (!_enemiesDamage.isDead)
        {
            _distanceToTarget = Vector3.Distance(transform.position, _target.position);
            _chaseComplete = _distanceToTarget < chasingDistanceThreshold;
            _fleeComplete = _distanceToTarget > fleeingDistanceThreshold;
        }
    }
}
