using System;
using System.Collections;
using UnityEngine;

public class WeaponSpecs : MonoBehaviour
{
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private float shootDelay = 0.5f;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] bulletSounds;
    
    private GameObject _character;
    private CharacterInput _characterInput;
    private CharacterStats _characterStats;
    private bool _delayAfterSwitch = false;
    public int id = 0;
    
    private ObjectsRef _objectsRef;
    private Aiming _aiming;

    [SerializeField] private int price;
    public int Price => price;
    
    
    private bool _canShoot;

    private Coroutine _shootCoroutine;
    public GameObject ObjectPrefab => objectPrefab;

   
    void Start()
    {
        _canShoot = true;
        _character = GameObject.FindGameObjectWithTag("Player");
        _characterInput = GetComponentInParent<CharacterInput>();
        _characterStats = GetComponentInParent<CharacterStats>();
        _aiming = GetComponentInParent<Aiming>();
        _objectsRef = GetComponentInParent<ObjectsRef>();
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
            GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity, _objectsRef.transform);
            bullet.GetComponent<Bullets>().Damage += _characterStats.damage;

            audioSource.PlayOneShot(bulletSounds[UnityEngine.Random.Range(0, bulletSounds.Length)]);

            Quaternion direction;
            if (_character.transform.localScale.x > 0)
            {
                direction = Quaternion.Euler(0, 0, _aiming.rotZ);
            }
            else
            {
                direction = Quaternion.Euler(0, 0, _aiming.rotZ - 180);
            }
            bullet.GetComponentInChildren<SpriteRenderer>().transform.rotation = direction;
            
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
