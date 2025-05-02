using ProceduralLevelGenerator.Unity.Examples.Common;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Inventory>().keys++;
            Destroy(gameObject);
        }
    }
    
}
