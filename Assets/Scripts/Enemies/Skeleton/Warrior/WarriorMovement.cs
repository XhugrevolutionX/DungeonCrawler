using System;
using UnityEngine;

public class WarriorMovement : MonoBehaviour
{
      enum FSM_State
    {
        Empty,
        Idle,
        Chase,
    }
    
    [SerializeField] private Sensor playerSensor;
    
    private FSM_State _currentState = FSM_State.Empty;
    private SteeringBehaviour _motion;
    private float _timer;
    private EnemiesDamage _enemyDamage;

    private void Start()
    {
        _motion = GetComponent<SteeringBehaviour>();
        _enemyDamage = GetComponent<EnemiesDamage>();
        SetState(FSM_State.Idle);
    }

    private void Update()
    {
        Debug.Log(_currentState);
        CheckTransitions(_currentState);
        OnStateUpdate(_currentState);
    }

    private void CheckTransitions(FSM_State state)
    {
        switch (state)
        {
            case FSM_State.Idle:
                if (playerSensor.hasDetected && !_enemyDamage.isDead) SetState(FSM_State.Chase);
                break;
            case FSM_State.Chase:
                if (!playerSensor.hasDetected || _enemyDamage.isDead) SetState(FSM_State.Idle);
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
                _motion.SeekFactor = 1;
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
                _motion.SeekFactor = 0;
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
                break;
            case FSM_State.Chase:
                _motion.SteeringTarget = playerSensor.targetPos;
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
