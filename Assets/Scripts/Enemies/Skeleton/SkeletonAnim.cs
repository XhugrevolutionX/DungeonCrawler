using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SkeletonAnim : MonoBehaviour
{
    [SerializeField] private float deathAnimDelay = 0.5f;
    
    private AIPath _path;
    private Animator _animator;
    private SpriteRenderer _sr;
    private EnemiesDamage _enemyDamage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyDamage = GetComponent<EnemiesDamage>();
        _path = GetComponent<AIPath>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_path.velocity.magnitude > Mathf.Epsilon)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }

        if (_path.velocity.normalized.x > 0)
        {
            _sr.flipX = false;
        }
        else if (_path.velocity.normalized.x < 0)
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