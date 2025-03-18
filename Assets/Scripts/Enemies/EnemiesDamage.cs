using System.Collections;
using UnityEngine;

public class EnemiesDamage : MonoBehaviour
{
    
    [SerializeField] private int power = 1;
    [SerializeField] private int health = 3;
    
    private CapsuleCollider2D _collider;
    
    private SkeletonAnim _skeletonAnim;
    public int Power => power;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _skeletonAnim = GetComponent<SkeletonAnim>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }
    
    private void Death()
    {
        _skeletonAnim.isDead = true;
        _collider.enabled = false;
    }

}
