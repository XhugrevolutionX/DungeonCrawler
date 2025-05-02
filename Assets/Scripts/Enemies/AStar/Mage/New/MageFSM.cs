using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MageFSM : MonoBehaviour
{
      enum FSM_State
    {
        Empty,
        Idle,
        Chase,
        Flee,
    }
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float idleTime = 1f;  
    
    private FSM_State _currentState = FSM_State.Empty;
    private EnemyBehaviors _behaviors;
    private AIPath _aiPath;
    private EnemiesDamage _enemiesDamage;
    private MageSensor _sensor;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    
    private Collider2D _collider;
    private SpriteRenderer _renderer;
    
    private Vector3 _tpPosition;

    private float _timer;
 
    private void Start()
    {
        _behaviors = GetComponent<EnemyBehaviors>();
        _aiPath = GetComponent<AIPath>();
        _enemiesDamage = GetComponentInChildren<EnemiesDamage>();
        _sensor = GetComponent<MageSensor>();
        
        _collider = GetComponentInChildren<Collider2D>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();  
        
        _aiPath.maxSpeed = moveSpeed;
        
        SetState(FSM_State.Chase);
    }

    private void Update()
    {
        CheckTransitions(_currentState);
        OnStateUpdate(_currentState);
    }

    private void CheckTransitions(FSM_State state)
    {
        switch (state)
        {
            case FSM_State.Idle:
                if (_enemiesDamage.Damaged && !_enemiesDamage.isDead)
                    SetState(FSM_State.Flee);
                if (_timer >= idleTime || (!_sensor.PlayerInSight && _sensor.PlayerWasInSight))
                    SetState(FSM_State.Chase);
                break;
            case FSM_State.Chase:
                if ( _enemiesDamage.Damaged && !_enemiesDamage.isDead)
                    SetState(FSM_State.Flee);
                if (_sensor.PlayerInSight)
                    SetState(FSM_State.Idle);
                break;  
            case FSM_State.Flee: 
                if(_sensor.FleeComplete)
                    SetState(FSM_State.Idle);
                break; 
            case FSM_State.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void OnStateEnter(FSM_State state)
    { 
        switch (state)
        {
            case FSM_State.Idle:
                break;
            case FSM_State.Chase:
                break;
            case FSM_State.Flee:
                _collider.enabled = false;
                _animator.SetTrigger("Damaged");
                _rigidbody.bodyType = RigidbodyType2D.Static;
                break; 
            case FSM_State.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    
    private void OnStateExit(FSM_State state)
    {
        switch (state)
        {
            case FSM_State.Idle:
                _timer = 0;
                break;
            case FSM_State.Chase:
                break;
            case FSM_State.Flee:
                _collider.enabled = true;
                _renderer.enabled = true;
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _aiPath.canMove = true;
                break; 
            case FSM_State.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    
    private void OnStateUpdate(FSM_State state)
    {
        switch (state)
        {
            case FSM_State.Idle:
                _aiPath.destination = transform.position;
                if (!_sensor.PlayerInSight)
                {
                    _timer += Time.deltaTime;
                }
                break;
            case FSM_State.Chase:
                _aiPath.destination = _behaviors.Chase();
                break;
            case FSM_State.Flee:
                _aiPath.canMove = false;
                break; 
            case FSM_State.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    
    private void SetState(FSM_State newState)
    {
        if (newState == FSM_State.Empty || _currentState == newState) return;
        if (_currentState != FSM_State.Empty) OnStateExit(_currentState);
        
        _currentState = newState;
        
        OnStateEnter(_currentState);
    }

    public void Teleport()
    {
        _renderer.enabled = false;
        _tpPosition = _behaviors.RandomPosition();
        _sensor.TargetTp = _tpPosition;
        transform.position = _tpPosition;
    }
}
