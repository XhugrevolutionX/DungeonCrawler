using System;
using Pathfinding;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
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
    private EnemySensor _sensor;

    private float _timer;
 
    private void Start()
    {
        _behaviors = GetComponent<EnemyBehaviors>();
        _aiPath = GetComponent<AIPath>();
        _enemiesDamage = GetComponentInChildren<EnemiesDamage>();
        _sensor = GetComponent<EnemySensor>();
        
        
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
                if ( _enemiesDamage.Damaged)
                    SetState(FSM_State.Flee);
                if (_timer >= idleTime)
                    SetState(FSM_State.Chase);
                break;
            case FSM_State.Chase:
                if ( _enemiesDamage.Damaged)
                    SetState(FSM_State.Flee);
                if (_sensor.ChaseComplete)
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
                if (!_sensor.ChaseComplete)
                {
                    _timer += Time.deltaTime;
                }
                break;
            case FSM_State.Chase:
                _aiPath.destination = _behaviors.Chase();
                break;
            case FSM_State.Flee:
                _aiPath.destination = _behaviors.Flee();
                break; 
            case FSM_State.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    
    private void SetState(FSM_State newState)
    {
        if (newState == FSM_State.Empty) return;
        if (_currentState != FSM_State.Empty) OnStateExit(_currentState);
        
        _currentState = newState;
        
        OnStateEnter(_currentState);
    }
}
