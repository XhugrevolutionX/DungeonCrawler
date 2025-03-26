using System;
using Pathfinding;
using UnityEngine;

public class KamikazeFSM : MonoBehaviour
{
      enum FSM_State
    {
        Empty,
        Idle,
        Chase,
    }
    
    [SerializeField] private float moveSpeed;
    
    private FSM_State _currentState = FSM_State.Empty;
    private EnemyBehaviors _behaviors;
    private AIPath _aiPath;
    private KamikazeSensor _sensor;

    private float _timer;
 
    private void Start()
    {
        _behaviors = GetComponent<EnemyBehaviors>();
        _aiPath = GetComponent<AIPath>();
        _sensor = GetComponent<KamikazeSensor>();
        _aiPath.maxSpeed = moveSpeed;
        
        SetState(FSM_State.Idle);
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
                if (_sensor.PlayerInSight)
                    SetState(FSM_State.Chase);
                break;
            case FSM_State.Chase:
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
                break;
            case FSM_State.Chase:
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
                break;
            case FSM_State.Chase:
                _aiPath.destination = _behaviors.Chase();
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
