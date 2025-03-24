using System;
using System.Collections;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private EnemiesDamage enemiesDamage;
    [SerializeField] private Animator deathAnimator;
    
    void OnDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!enemiesDamage.isDead)
            {
                deathAnimator.SetTrigger("Explode");
            }
        }
    }
}