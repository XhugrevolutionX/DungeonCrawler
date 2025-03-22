using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    [SerializeField] private float timeBetweenAttacks = 2;
    [SerializeField] private GameObject bullets;
    [SerializeField] private GameObject firepoint;
    [SerializeField] private SpriteRenderer _sr;
    private bool _canShoot = true;
    private bool _playerDetected = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_sr.flipX)
        {
            firepoint.transform.localPosition = new Vector3(-0.5f, 0,0);
        }
        else if (!_sr.flipX)
        {
            firepoint.transform.localPosition = new Vector3(0.5f, 0,0);
        }
        else
        {
            
        }

        if (_playerDetected)
        {
            if (_canShoot)
            {
                Instantiate(bullets, firepoint.transform.position, Quaternion.identity);
                //Shoots
                _canShoot = false;
                StartCoroutine("FireRate");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerDetected = false;
        }
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        _canShoot = true;
    }
}