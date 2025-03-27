using System.Collections;
using UnityEngine;

public class EnemiesDamage : MonoBehaviour
{
    
    [SerializeField] private int power = 1;
    [SerializeField] private int health = 3;
    
    private CapsuleCollider2D _collider;
    public bool isDead = false;
    
    private bool _damaged = false;
    public bool Damaged => _damaged;
    public int Power => power;
    
    private Coroutine _damageCoroutine;
    
    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
    }
    

    public void Hit(int damage)
    {
        health -= damage;
        _damaged = true;
        
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
        }
        _damageCoroutine = StartCoroutine(DamagedCoroutine());
        
        if (health <= 0)
        {
            Death();
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
