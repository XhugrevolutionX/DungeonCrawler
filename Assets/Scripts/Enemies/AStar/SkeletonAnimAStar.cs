using System.Collections;
using Pathfinding;
using UnityEngine;

public class SkeletonAnimAStar : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private EnemiesDamage enemyDamage;
    
    private AIPath _path;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _path = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_path.velocity.magnitude > Mathf.Epsilon)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (_path.velocity.normalized.x > 0)
        {
            sr.flipX = false;
        }
        else if (_path.velocity.normalized.x < 0)
        {
            sr.flipX = true;
        }
        else if (_path.velocity.normalized.x == 0)
        {
            
        }

        if (enemyDamage.isDead)
        {
            _path.canMove = false;
            animator.SetTrigger("Death");
        }
    }
}