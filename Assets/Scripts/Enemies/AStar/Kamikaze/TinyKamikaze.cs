using UnityEngine;

public class TinyKamikaze : MonoBehaviour
{
    private AIAgent _agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<AIAgent>();
        _agent.Target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
