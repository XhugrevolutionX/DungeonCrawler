using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Chest : MonoBehaviour
{
    private ObjectsRef _objectsRef;
    [SerializeField] private Transform spawnPoint;
    
    private Animator _animator;
    private Canvas _canvas;
    
    private CharacterInput _characterInput;
    private Inventory _characterInventory;

    private bool _open = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _canvas = GetComponentInChildren<Canvas>();
        _canvas.enabled = false;
        _objectsRef = FindFirstObjectByType<ObjectsRef>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_open)
            {
                _canvas.enabled = true;
                _characterInput = other.gameObject.GetComponent<CharacterInput>();
                _characterInventory = other.gameObject.GetComponent<Inventory>();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_characterInput.inputAction)
            {
                if (_open == false)
                {
                    if (_characterInventory.keys > 0)
                    {
                        OpenChest();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canvas.enabled = false;
        }
    }

    private void OpenChest()
    {
        _open = true;
        _animator.SetTrigger("opened");
        _characterInventory.keys -= 1;
        _canvas.enabled = false;
    }

    private void InstantiateWeapon()
    {
        List<int> playerWeaponsIds = _characterInventory.GetWeaponsIds();

        if (playerWeaponsIds.Count >= _objectsRef.Weapons.Length)
        {
            InstantiateObject();
        }
        else
        {
            int rnd;
            do
            {
                rnd = UnityEngine.Random.Range(0, _objectsRef.Weapons.Length);
                
            } while (playerWeaponsIds.Contains(rnd));
            
            Instantiate(_objectsRef.Weapons[rnd], spawnPoint.position, Quaternion.identity);
        }
    }

    private void InstantiateObject()
    {
        int rnd = UnityEngine.Random.Range(0, _objectsRef.Items.Length);
            
        Instantiate(_objectsRef.Items[rnd], spawnPoint.position, Quaternion.identity);
    }
    

    public void InstantiateChestReward()
    {
        int rnd = UnityEngine.Random.Range(0, 100);

        if (rnd < 50)
        {
            InstantiateWeapon();
        }
        else
        {
            InstantiateObject();
        }
    }
}
