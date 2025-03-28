using System.Collections;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesDamage : MonoBehaviour
{
    
    [SerializeField] private int power = 1;
    [SerializeField] private int health = 3;
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
    
    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _aiPath = GetComponentInParent<AIPath>();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        if (_timerIsRunning)
        {
            _timer += Time.deltaTime;
            if (_timer > _timerLimit)
            {
                _aiPath.canMove = true;
                _timer = 0;
                _timerIsRunning = false;
                _rigidbody.linearVelocity = Vector2.zero;
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
            _timerIsRunning = true;
            _rigidbody.AddForce(knockBack, ForceMode2D.Impulse);
        }
    }
    
    private void Death()
    {
        isDead = true;
        _collider.enabled = false;
    }
    
    private void DestroySelf()
    {
        Destroy(transform.parent.gameObject);
    }

    IEnumerator DamagedCoroutine()
    {
        yield return new WaitForSeconds(0.005f);
        _damaged = false;

    }

}
