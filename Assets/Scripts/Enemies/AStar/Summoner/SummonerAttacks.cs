using UnityEngine;
using UnityEngine.Tilemaps;

public class SummonerAttacks : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject summons;
    [SerializeField] private float summonRange = 4;
    [SerializeField] private Tilemap groundTilemap;
    public bool hasSummoned = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        } while (!groundTilemap.HasTile(groundTilemap.WorldToCell(position)));
        
        Instantiate(summons, position, Quaternion.identity);
        hasSummoned = true;
    }
}
