using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private HealthObject baseHealthObject;
    [SerializeField] private HealthObject characterHealthObject;
    
    [SerializeField] private float deathDelay = 2;
    [SerializeField] private float iFramesDelay = 1;
    [SerializeField] private NewHealthBar healthBar;
    
    [SerializeField] private bool heal;
    [SerializeField] private bool damage;
    
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    private int _maxHealthLimit = 20;
    
    private Game _game;
    
    private Animator _animator;
    
    private Coroutine _iFramesCoroutine;

    private bool _canBeHit;
    
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canBeHit = true;
        _animator = GetComponent<Animator>();
        _game = GetComponentInParent<Game>();
        
        health = characterHealthObject.Health;
        maxHealth = characterHealthObject.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //For Editor Tests
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
            healthBar.UpdateHealthBar(health, maxHealth);
            if (health <= 0)
            {
                StartCoroutine(DeathDelay());
            }
        }
    }

    public void Heal(int heal)
    {
        if (health + heal <= maxHealth)
        {
            health += heal;
        }
        else
        {
            health = maxHealth;
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    
    public void AddMaxHealth(int hearth)
    {
        if (maxHealth + (hearth * 2) <= _maxHealthLimit)
        {
            maxHealth += (hearth * 2) ;
        }
        else
        {
            maxHealth = _maxHealthLimit;
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    private void Death()
    {
        _game.EndRun(false);
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

    public void SaveHealthData()
    {
        characterHealthObject.Health = health;
        characterHealthObject.MaxHealth = maxHealth;
    }

    public void ResetHealthData()
    {
        characterHealthObject.Health = baseHealthObject.Health;
        characterHealthObject.MaxHealth = baseHealthObject.MaxHealth;
    }
}