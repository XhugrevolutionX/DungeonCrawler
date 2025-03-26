using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KamikazeSensor : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float angleDetection = 45;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string playerTag;

    [SerializeField] private Color gizomsColor = Color.red;
    
    private Vector2 _hitPosition;
    private List<Collider2D> _colliders = new List<Collider2D>();
    private ContactFilter2D _contactFilter;
    
    private bool _playerInSight = false;

    public bool PlayerInSight => _playerInSight;

    public void OnDrawGizmos()
    {
        Gizmos.color = _playerInSight ? Color.green : gizomsColor;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, angleDetection) * transform.right * radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -angleDetection) * transform.right * radius);
        Gizmos.DrawLine(transform.position, _hitPosition);
    }

    private void Start()
    {
         _contactFilter.SetLayerMask(layerMask);
         _contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        _playerInSight = false;
        _hitPosition = transform.position;

        _colliders.Clear();
        
        Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), radius, _contactFilter, _colliders);

        Collider2D goodObject = _colliders.FirstOrDefault(c => c.CompareTag(playerTag));
        if (goodObject != null)
        {
            Vector2 goodObjectDistance = goodObject.bounds.center - transform.position;
            float angle = Vector2.Angle(transform.right, goodObjectDistance);
            if (angle < angleDetection)
            {
                List<RaycastHit2D> hits = new List<RaycastHit2D>();
                
                if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), goodObjectDistance, _contactFilter, hits, radius) > 0)
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