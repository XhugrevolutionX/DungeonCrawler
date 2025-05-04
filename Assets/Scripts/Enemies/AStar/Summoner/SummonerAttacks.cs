using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SummonerAttacks : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject summons;
    [SerializeField] private float summonRange = 4;
    private Game _game;
    private Tilemap _groundTilemap;
    private EnemiesDamage _enemyDamage;
    public bool hasSummoned = false;

    private void Start()
    {
        _game = GetComponentInParent<Game>();
        _groundTilemap = _game.GroundTilemap;
        _enemyDamage = GetComponentInChildren<EnemiesDamage>();
    }


    private void Update()
    {
        if (_enemyDamage.Damaged)
            animator.SetTrigger("Damaged");
    }

    public void SummonAnimation()
    {
        animator.SetTrigger("Summon");
    }
    
    
    private void Summon()
    {
        Vector3 position;
        do
        { 
            position = transform.position + new Vector3(Random.Range(-summonRange, summonRange), Random.Range(-summonRange, summonRange), 0);
        } while (!_groundTilemap.HasTile(_groundTilemap.WorldToCell(position)));
        
        Instantiate(summons, position, Quaternion.identity, transform.parent);
        hasSummoned = true;
    }
}
