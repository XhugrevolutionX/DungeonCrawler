using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SummonerAttacks : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject summons;
    [SerializeField] private float summonRange = 4;
    private EnemyManager _manager;
    private Tilemap _groundTilemap;
    private EnemiesDamage _enemyDamage;
    public bool hasSummoned = false;

    private void Start()
    {
        _manager = GetComponentInParent<EnemyManager>();
        _groundTilemap = _manager.GroundTilemap;
        _enemyDamage = GetComponent<EnemiesDamage>();
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
        
        Instantiate(summons, position, Quaternion.identity, _manager.transform);
        hasSummoned = true;
    }
}
