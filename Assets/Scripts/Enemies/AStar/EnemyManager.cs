using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemManager : MonoBehaviour
{
    
    private GameObject _player;
    private Tilemap _groundTilemap;

    public Tilemap GroundTilemap => _groundTilemap;
    public GameObject Player => _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _groundTilemap = GameObject.FindWithTag("Ground").GetComponent<Tilemap>();
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
