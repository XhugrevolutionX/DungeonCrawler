using UnityEngine;

public class Rewards : MonoBehaviour
{
    [SerializeField] private GameObject[] foodsPrefabs;
    [SerializeField] private GameObject keyPrefabs;

    public GameObject[] Foods => foodsPrefabs;
    public GameObject Key => keyPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
