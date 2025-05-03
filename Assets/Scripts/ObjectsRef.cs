using UnityEngine;

public class ObjectsRef : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponsPrefabs;
    [SerializeField] private GameObject[] foodsPrefabs;
    [SerializeField] private GameObject keyPrefabs;

    public GameObject[] Weapons => weaponsPrefabs;
    public GameObject[] Foods => foodsPrefabs;
    public GameObject Key => keyPrefabs;

    
}
