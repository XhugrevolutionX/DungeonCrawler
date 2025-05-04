using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectsRef : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponsPrefabs;
    [SerializeField] private GameObject[] foodsPrefabs;
    [SerializeField] private GameObject[] coinsPrefabs;
    [SerializeField] private GameObject keyPrefabs;


    
    public GameObject[] Weapons => weaponsPrefabs;
    public GameObject[] Foods => foodsPrefabs;
    public GameObject[] Coins => coinsPrefabs;
    public GameObject Key => keyPrefabs;
    
}
