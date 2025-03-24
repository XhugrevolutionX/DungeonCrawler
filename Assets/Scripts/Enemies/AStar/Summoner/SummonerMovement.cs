using System;
using UnityEngine;

public class SummonerMovement : MonoBehaviour
{
    enum FSM_State
    {
        Empty,
        Summon,
        Reposition,
    }
    [SerializeField] private EnemiesDamage enemyDamage;
    private AIAgent _ai;
    private SummonerDestination _destination;
    private SummonerAttacks _attacks;
    private FSM_State _currentState = FSM_State.Empty;

    private void Start()
    {
        _ai = GetComponent<AIAgent>();
        _destination = GetComponent<SummonerDestination>();
        _attacks = GetComponentInChildren<SummonerAttacks>();
        SetState(FSM_State.Summon);
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
            case FSM_State.Summon:
                if (_attacks.hasSummoned || enemyDamage.Damaged)
                {
                    SetState(FSM_State.Reposition);
                }
                break;
            case FSM_State.Reposition:
                if (_destination.hasReachedDestination)
                {
                    SetState(FSM_State.Summon);
                }
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
            case FSM_State.Summon:
                _attacks.SummonAnimation();
                break;
            case FSM_State.Reposition:
                _ai.Target = _destination.NewDestination();
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
            case FSM_State.Summon:
                _attacks.hasSummoned = false;
                break;
            case FSM_State.Reposition:
                _ai.Target = transform;
                _destination.hasReachedDestination = false;
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
            case FSM_State.Summon:
                break;
            case FSM_State.Reposition:
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
