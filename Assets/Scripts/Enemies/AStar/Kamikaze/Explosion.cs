using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int playerDamage = 1;
    [SerializeField] private int enemyDamage = 10;
    [SerializeField] private float knockBackForce = 8;

    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 direction = other.transform.position - transform.position;
        
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterHealth>().Damage(playerDamage);
        }
        if (other.CompareTag("Enemies"))
        {
            other.gameObject.GetComponent<EnemiesDamage>().Hit(enemyDamage, direction * knockBackForce);
        }
    }
}
