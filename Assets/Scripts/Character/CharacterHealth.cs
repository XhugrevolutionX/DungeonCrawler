using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float deathDelay = 2;
    [SerializeField] private float iFramesDelay = 1;
    [SerializeField] private NewHealthBar healthBar;
    
    [SerializeField] private bool heal;
    [SerializeField] private bool damage;
    
    private CharacterStats _characterStats;
    
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
        _characterStats = GetComponent<CharacterStats>();

        _characterStats.LoadStatsData();
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
            
            _characterStats.health -= damage;
            healthBar.UpdateHealthBar(_characterStats.health, _characterStats.maxHealth);
            if (_characterStats.health <= 0)
            {
                StartCoroutine(DeathDelay());
            }
        }
    }

    public void Heal(int heal)
    {
        if (_characterStats.health + heal <= _characterStats.maxHealth)
        {
            _characterStats.health += heal;
        }
        else
        {
            _characterStats.health = _characterStats.maxHealth;
        }
        healthBar.UpdateHealthBar(_characterStats.health, _characterStats.maxHealth);
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
}