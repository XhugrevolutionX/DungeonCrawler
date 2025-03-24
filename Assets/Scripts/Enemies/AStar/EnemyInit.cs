using TMPro;
using UnityEngine;

public class EnemyInit : MonoBehaviour
{
    private AIAgent _agent;
    private EnemyManager _manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<AIAgent>();
        _manager = GetComponentInParent<EnemyManager>();
        if (_manager.Player.activeInHierarchy)
        {
            _agent.Target = _manager.Player.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
