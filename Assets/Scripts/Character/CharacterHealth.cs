using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private float deathDelay = 2;
    [SerializeField] private float iFramesDelay = 1;
    [SerializeField] private HealthBar healthBar;
    
    [SerializeField] private bool heal;
    [SerializeField] private bool damage;
    
    private int _maxHealth = 10;
    
    private Animator _animator;
    
    private Coroutine _iFramesCoroutine;

    private bool _canBeHit;
    public int Health => health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.InitializeHealthBar(health);
        _canBeHit = true;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heal)
        {
            Heal(1);
            heal = false;
        }

        if (damage)
        {
            Damage(1);
            damage = false;
        }
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemies"))
        {
            collision.collider.TryGetComponent(out EnemiesDamage e);
            if (e != null)
                Damage(e.Power);
        }
    }

    public void Damage(int damage)
    {
        if (_canBeHit)
        {
            _canBeHit = false;
            
            _animator.SetTrigger("Damaged");
            
            if (_iFramesCoroutine != null)
                StopCoroutine(_iFramesCoroutine);
            _iFramesCoroutine = StartCoroutine(IFrames());
            
            health -= damage;
            healthBar.UpdateHealthBar(health);
            if (health <= 0)
            {
                StartCoroutine(DeathDelay());
            }
        }
    }

    public void Heal(int heal)
    {
        if (health + heal <= _maxHealth)
        {
            health += heal;
        }
        else
        {
            health = _maxHealth;
        }
        healthBar.UpdateHealthBar(health);
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        Death();
    }
    IEnumerator IFrames()
    {
        yield return new WaitForSeconds(iFramesDelay);
        _canBeHit = true;
    }
    
    
}