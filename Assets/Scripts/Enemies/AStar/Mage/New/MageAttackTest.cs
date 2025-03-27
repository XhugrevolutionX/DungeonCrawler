using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MageAttackTest : MonoBehaviour
{
    [SerializeField] private float timeBetweenAttacks = 2;
    [SerializeField] private GameObject bullets;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private SpriteRenderer sr;
    private MageSensor _sensor;
    
    private bool _canShoot = false;

    private Coroutine _shootCoroutine;
    
    private void Start()
    {
        _sensor = GetComponent<MageSensor>();
    }

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
        
        if (_sensor.PlayerInSight)
        {
            if (!_sensor.PlayerWasInSight)
            {
                if (_shootCoroutine != null)
                {
                    StopCoroutine(_shootCoroutine);
                }
                _shootCoroutine = StartCoroutine("FireRate");
            }
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
        else
        {
            if (_sensor.PlayerWasInSight)
            {
                if (_shootCoroutine != null)
                {
                    StopCoroutine(_shootCoroutine);
                }
                _canShoot = false;
            }
        }
    }
    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        _canShoot = true;
    }
}