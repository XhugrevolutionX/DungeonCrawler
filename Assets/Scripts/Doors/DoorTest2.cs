using UnityEngine;

public class DoorTest2 : MonoBehaviour
{
    
    [SerializeField] private DoorManager doorManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doorManager.HasDoor(transform.position);
    }
}
