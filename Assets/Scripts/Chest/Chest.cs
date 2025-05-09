using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Chest : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    
    private ObjectsRef _objectsRef;
    private ShopItemsManager _shopItems;
    
    private CharacterInput _characterInput;
    private Inventory _characterInventory;
    
    private Canvas _canvas;
    
    private Animator _animator;

    private bool _open = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _canvas = GetComponentInChildren<Canvas>();
        _canvas.enabled = false;
        _objectsRef = GetComponentInParent<ObjectsRef>();
        _shopItems = FindFirstObjectByType<ShopItemsManager>();
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

    public void ChoseChestReward()
    {
        int rnd = UnityEngine.Random.Range(0, 100);
        
        //50% chance for a weapons or an item
        if (rnd < 50)
        {
            InstantiateWeapon();
        }
        else
        {
            InstantiateObject();
        }
    }

    private void InstantiateWeapon()
    {
        List<int> playerWeaponsIds = _characterInventory.GetWeaponsIds();
        List<int> shopWeaponsIds = _shopItems.GetShopWeaponsIds();
        
        List<int> playerItemsIds = _characterInventory.GetItemsIds();
        List<int> shopItemsIds = _shopItems.GetShopItemsIds();
        
        //Check if all the weapons have already been spawned
        if (playerWeaponsIds.Count + shopWeaponsIds.Count >= _objectsRef.Weapons.Length)
        {
            //Check if all the items have already been spawned
            if (playerItemsIds.Count + shopItemsIds.Count >= _objectsRef.Items.Length)
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
            //Find a weapon neither the player nor the shop already have
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

        //Check if all the items have already been spawned
        if (playerItemsIds.Count + shopItemsIds.Count >= _objectsRef.Items.Length)
        {
            //Check if all the weapons have already been spawned
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
            //Find an item neither the player nor the shop already have
            do
            {
                rnd = UnityEngine.Random.Range(0, _objectsRef.Items.Length);
                
            } while (playerItemsIds.Contains(rnd) || shopItemsIds.Contains(rnd));
            
            Instantiate(_objectsRef.Items[rnd], spawnPoint.position, Quaternion.identity);
        }
    }
    
}
