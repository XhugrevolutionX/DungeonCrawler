using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectsRef : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponsPrefabs;
    [SerializeField] private GameObject[] foodsPrefabs;
    [SerializeField] private GameObject[] coinsPrefabs;
    [SerializeField] private GameObject keyPrefabs;

    private GameObject _player;
    private Tilemap _groundTilemap;

    public GameObject Player => _player;
    public Tilemap GroundTilemap
    {
        get => _groundTilemap;
        set => _groundTilemap = value;
    }
    
    public GameObject[] Weapons => weaponsPrefabs;
    public GameObject[] Foods => foodsPrefabs;
    public GameObject[] Coins => coinsPrefabs;
    public GameObject Key => keyPrefabs;
    
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }
}
