using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    [SerializeField] private float timeBetweenAttacks = 2;
    [SerializeField] private GameObject bullets;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private SpriteRenderer sr;
    private bool _canShoot = false;
    private bool _playerDetected = false;

    private Coroutine _shootCoroutine;

    // Update is called once per frame
    void Update()
    {
        if (sr.flipX)
        {
            firePoint.transform.localPosition = new Vector3(-0.5f, 0,0);
        }
        else if (!sr.flipX)
        {
            firePoint.transform.localPosition = new Vector3(0.5f, 0,0);
        }

        if (_playerDetected)
        {
            if (_canShoot)
            {
                Instantiate(bullets, firePoint.transform.position, Quaternion.identity);
                _canShoot = false;
                if (_shootCoroutine != null)
                {
                    StopCoroutine(_shootCoroutine);
                }
                _shootCoroutine = StartCoroutine("FireRate");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }
            _shootCoroutine = StartCoroutine("FireRate");
            _playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }
            _canShoot = false;
            _playerDetected = false;
        }
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        _canShoot = true;
    }
}