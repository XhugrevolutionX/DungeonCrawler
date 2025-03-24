using UnityEngine;
using Pathfinding;
public class AIAgent : MonoBehaviour
{
    private AIPath _aiPath;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    private float _distanceToTarget;
    [SerializeField] private float stoppingDistanceThreshold;
    private EnemiesDamage _enemiesDamage;
    public Transform Target
    {
        get => target;
        set => target = value;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _enemiesDamage = GetComponentInChildren<EnemiesDamage>();
        _aiPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_enemiesDamage.isDead)
        {
            _aiPath.maxSpeed = moveSpeed;
            _distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (_distanceToTarget < stoppingDistanceThreshold)
            {
                //Chase when the player is far
                _aiPath.destination=transform.position;
            
                //Chase when player is near
                // aiPath.destination = target.position;
            }
            else
            {
                //Chase when the player is far
                _aiPath.destination = target.position;
            
                //Chase when player is near
                // aiPath.destination=transform.position;
            }
        }
    }
}
