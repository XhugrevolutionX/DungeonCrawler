using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class MageSensor : MonoBehaviour
{
    private EnemiesDamage _enemiesDamage;
    private float _distanceToTarget;
    private Vector3 _targetTp;

    public Vector3 TargetTp
    {
        get => _targetTp;
        set => _targetTp = value;
    }

    private bool _fleeComplete;
    public bool FleeComplete => _fleeComplete;
    
    [SerializeField] private float inSightRadius;
    [SerializeField] private float angleDetection = 180;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string playerTag;
    
    private Vector2 _hitPosition;
    private List<Collider2D> _colliders = new List<Collider2D>();
    private ContactFilter2D _contactFilter;
    
    private bool _playerInSight = false;
    private bool _playerWasInSight = false;

    public bool PlayerWasInSight => _playerWasInSight;
    public bool PlayerInSight => _playerInSight;

    [SerializeField] private bool drawGizmos;
    [SerializeField] private Color gizmosColor = Color.red;
    
    public void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = _playerInSight ? Color.green : gizmosColor;
            Gizmos.DrawWireSphere(transform.position, inSightRadius);
            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, angleDetection) * transform.right * inSightRadius);
            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -angleDetection) * transform.right * inSightRadius);
            Gizmos.DrawLine(transform.position, _hitPosition);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _enemiesDamage = GetComponentInChildren<EnemiesDamage>();
        _contactFilter.SetLayerMask(layerMask);
    }

    void Update()
    {
        _playerWasInSight = _playerInSight;
        
        StateSensor();
        CheckIfPlayerInSight();
    }

    private void StateSensor()
    {
        if (!_enemiesDamage.isDead)
        {
            _fleeComplete = transform.position == _targetTp;
        }
    }
    
    private void CheckIfPlayerInSight()
    {
        _hitPosition = transform.position;
        _playerInSight = false;
        _colliders.Clear();
        
        Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), inSightRadius, _contactFilter, _colliders);

        Collider2D goodObject = _colliders.FirstOrDefault(c => c.CompareTag(playerTag));
        if (goodObject != null)
        {
            Vector2 goodObjectDistance = goodObject.bounds.center - transform.position;
            float angle = Vector2.Angle(transform.right, goodObjectDistance);
            if (angle < angleDetection)
            {
                List<RaycastHit2D> hits = new List<RaycastHit2D>();
                
                if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), goodObjectDistance, _contactFilter, hits, inSightRadius) > 0)
                {
                    if (hits[0].collider == goodObject)
                    {
                        _hitPosition = hits[0].point;
                        _playerInSight = true;
                    }
                }
            }
        }
    }
}
