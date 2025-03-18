using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SteeringBehaviour : MonoBehaviour
{
    
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float steeringDamp = 0.5f;
    
    [Header("Seek")]
    [SerializeField] private float seekFactor = 1f;
    
    [Header("Flee")]
    [SerializeField] private float fleeFactor = 1f;
    
    private Rigidbody2D _rb;
    
    public Vector2 SteeringTarget { get; set; }
    public float SeekFactor    {
        get => seekFactor;
        set => seekFactor = value;
    }
    public float FleeFactor    {
        get => fleeFactor;
        set => fleeFactor = value;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 steeringResult = Vector2.zero;

        if (seekFactor > 0) 
            steeringResult += seekFactor * Seek(SteeringTarget);
        if (fleeFactor > 0) 
            steeringResult += fleeFactor * Flee(SteeringTarget);

        _rb.linearVelocity = steeringResult;

        // Max speed
        if (_rb.linearVelocity.magnitude > maxSpeed)
            _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
    }

    Vector2 Seek(Vector2 targetPosition)
    {
        Vector2 steeringForce = Vector2.zero;
        
        Vector2 actualVelocity = _rb.linearVelocity;
        Vector2 desiredVelocity = (targetPosition - new Vector2(transform.position.x, transform.position.y)).normalized * maxSpeed;
        
        steeringForce = (desiredVelocity - actualVelocity) * steeringDamp;
        
        // Debug draws
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), desiredVelocity, Color.magenta);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), actualVelocity, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y) + actualVelocity, steeringForce, Color.green);
        
        return steeringForce;
    }
    
    Vector2 Flee(Vector2 targetPosition)
    {
        
        Vector2 steeringForce = Vector2.zero;
        
        Vector2 actualVelocity = _rb.linearVelocity;
        Vector2 desiredVelocity = ( new Vector2(transform.position.x, transform.position.y) - targetPosition).normalized * maxSpeed;
        
        steeringForce = (desiredVelocity - actualVelocity) * steeringDamp;
        
        // Debug draws
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), desiredVelocity, Color.magenta);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), actualVelocity, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y) + actualVelocity, steeringForce, Color.green);
        
        return steeringForce;

    }
}
