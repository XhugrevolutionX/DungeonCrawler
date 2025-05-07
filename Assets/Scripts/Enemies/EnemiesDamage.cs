using System;
using System.Collections;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesDamage : MonoBehaviour
{
    
    [SerializeField] private int power = 1;
    [SerializeField] private int health = 3;
    
    [SerializeField] private int id;
    
    
    private Animator _animator;
    private AIPath _aiPath;
    private Rigidbody2D _rigidbody;
    
    private float _timer;
    private float _timerLimit = 0.2f;
    private bool _timerIsRunning = false;
    
    private CapsuleCollider2D _collider;
    public bool isDead = false;
    
    private bool _damaged = false;
    public bool Damaged => _damaged;
    public int Power => power;
    
    private Coroutine _damageCoroutine;
    
    private ObjectsRef _objectsRef;
    
    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _aiPath = GetComponentInParent<AIPath>();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        _objectsRef = GetComponentInParent<ObjectsRef>();
    }

    void Update()
    {
        if (_timerIsRunning)
        {
            _timer += Time.deltaTime;
            if (_timer > _timerLimit)
            {
                _rigidbody.linearVelocity = Vector2.zero;
                _aiPath.canMove = true;
                _timer = 0;
                _timerIsRunning = false;
            }
        }
    }
    

    public void Hit(int damage, Vector2 knockBack)
    {
        health -= damage;
        _damaged = true;
        
        _aiPath.canMove = false;
        
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
        }
        _damageCoroutine = StartCoroutine(DamagedCoroutine());

        if (health <= 0)
        {
            Death();
        }
        else
        {
            _animator.SetTrigger("Damaged");
            if (id != 1)
            {
                _timerIsRunning = true;
                _rigidbody.AddForce(knockBack, ForceMode2D.Impulse);
            }
        }
    }
    
    private void Death()
    {
        isDead = true;
        _collider.enabled = false;
    }
    
    private void DestroySelf()
    {
        if (id != 2)
        {
            //40% chance to spawn a coins 
            int rnd = UnityEngine.Random.Range(0, 100);
            if (rnd < 40)
            {
                //60% for a penny 30% for a dime 10% for a nickel
                rnd = UnityEngine.Random.Range(0, 100);
                if (rnd < 60)
                {
                    Instantiate(_objectsRef.Coins[0], transform.position, Quaternion.identity);
                }
                else if (rnd < 90)
                {
                    Instantiate(_objectsRef.Coins[1], transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(_objectsRef.Coins[2], transform.position, Quaternion.identity);
                }
                
            }
        }
        Destroy(transform.parent.gameObject);
    }

    IEnumerator DamagedCoroutine()
    {
        yield return new WaitForSeconds(0.005f);
        _damaged = false;

    }

}
