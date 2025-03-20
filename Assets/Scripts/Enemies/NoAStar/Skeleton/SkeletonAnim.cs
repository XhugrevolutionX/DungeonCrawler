using System.Collections;
using UnityEngine;

public class SkeletonAnim : MonoBehaviour
{
    [SerializeField] private float deathAnimDelay = 0.5f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;
    private EnemiesDamage _enemyDamage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyDamage = GetComponent<EnemiesDamage>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb.linearVelocity.magnitude > Mathf.Epsilon)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }

        if (_rb.linearVelocity.normalized.x > 0)
        {
            _sr.flipX = false;
        }
        else if (_rb.linearVelocity.normalized.x < 0)
        {
            _sr.flipX = true;
        }

        if (_enemyDamage.isDead)
        {
            _animator.SetTrigger("Death");
            StartCoroutine("DeathCoroutine");
        }
    }


    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(deathAnimDelay);
        Destroy(gameObject);
    }
}