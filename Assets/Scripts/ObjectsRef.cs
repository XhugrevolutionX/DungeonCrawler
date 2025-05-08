using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectsRef : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponsPrefabs;
    [SerializeField] private GameObject[] itemsPrefabs;
    [SerializeField] private GameObject[] foodsPrefabs;
    [SerializeField] private GameObject[] coinsPrefabs;
    [SerializeField] private GameObject[] effectsPrefabs;
    [SerializeField] private GameObject keyPrefabs;


    
    public GameObject[] Weapons => weaponsPrefabs;
    public GameObject[] Items => itemsPrefabs;
    public GameObject[] Foods => foodsPrefabs;
    public GameObject[] Coins => coinsPrefabs;
    public GameObject[] Effects => effectsPrefabs;
    public GameObject Key => keyPrefabs;
    
}
