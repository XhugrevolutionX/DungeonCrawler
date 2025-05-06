using System;
using System.Collections;
using UnityEngine;

public class WeaponSpecs : MonoBehaviour
{
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private float shootDelay = 0.5f;
    private CharacterInput _characterInput;
    private bool _delayAfterSwitch = false;
    public int id = 0;

    [SerializeField] private int price;
    public int Price => price;
    
    
    private bool _canShoot;

    private Coroutine _shootCoroutine;
    public GameObject Object => objectPrefab;

   
    void Start()
    {
        _canShoot = true;
        _characterInput = GetComponentInParent<CharacterInput>();
    }

    void OnEnable()
    {
        if (_delayAfterSwitch)
        {
            _shootCoroutine = StartCoroutine("ShootDelay");
        }
    }

    void OnDisable()
    {
       _delayAfterSwitch = !_canShoot;
       if (_shootCoroutine != null)
       {
           StopCoroutine(_shootCoroutine);
       }
    }

    private void Update()
    {
        if (_characterInput.inputShoot && _canShoot)
        {
            Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            _canShoot = false;

            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }

            _shootCoroutine = StartCoroutine("ShootDelay");
        }
    }
    
    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        _canShoot = true;
    }
}
