using System;
using System.Collections;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [SerializeField] private float timeBeforeExplosion = 1;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject kamikaze;
    
    
    private bool _explode = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void Update()
    {
        if (_explode)
        {
            Destroy(kamikaze);
        }
    }

    void OnDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("KamikazeCoroutine");
        }
    }

    private IEnumerator KamikazeCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeExplosion);
        _explode = true;
    }
}