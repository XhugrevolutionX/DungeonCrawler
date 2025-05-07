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
    private ShopItemsManager _shopItems;

    private bool _open = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _canvas = GetComponentInChildren<Canvas>();
        _canvas.enabled = false;
        _objectsRef = FindFirstObjectByType<ObjectsRef>();
        _shopItems = FindFirstObjectByType<ShopItemsManager>();
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
        List<int> shopWeaponsIds = _shopItems.GetShopWeaponsIds();
        
        List<int> playerItemsIds = _characterInventory.GetItemsIds();
        List<int> shopItemsIds = _shopItems.GetShopItemsIds();

        if (playerWeaponsIds.Count + shopWeaponsIds.Count >= _objectsRef.Weapons.Length)
        {
            if (playerItemsIds.Count + shopItemsIds.Count >= _objectsRef.Weapons.Length)
            {
                Debug.Log("All Items and Weapons have already been spawned");
            }
            else
            {
                InstantiateObject();
            }
        }
        else
        {
            int rnd;
            do
            {
                rnd = UnityEngine.Random.Range(0, _objectsRef.Weapons.Length);
                
            } while (playerWeaponsIds.Contains(rnd) || shopWeaponsIds.Contains(rnd));
            
            Instantiate(_objectsRef.Weapons[rnd].GetComponent<WeaponSpecs>().ObjectPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private void InstantiateObject()
    {
        
        List<int> playerItemsIds = _characterInventory.GetItemsIds();
        List<int> shopItemsIds = _shopItems.GetShopItemsIds();
        
        List<int> playerWeaponsIds = _characterInventory.GetWeaponsIds();
        List<int> shopWeaponsIds = _shopItems.GetShopWeaponsIds();

        if (playerItemsIds.Count + shopItemsIds.Count >= _objectsRef.Weapons.Length)
        {
            if (playerWeaponsIds.Count + shopWeaponsIds.Count >= _objectsRef.Weapons.Length)
            {
                Debug.Log("All Items and Weapons have already been spawned");
            }
            else
            {
                InstantiateWeapon();
            }
        }
        else
        {
            int rnd;

            do
            {
                rnd = UnityEngine.Random.Range(0, _objectsRef.Items.Length);
                
            } while (playerItemsIds.Contains(rnd) || shopItemsIds.Contains(rnd));
            
            Instantiate(_objectsRef.Items[rnd], spawnPoint.position, Quaternion.identity);
        }
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
