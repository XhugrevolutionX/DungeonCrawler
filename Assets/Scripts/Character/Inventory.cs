using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryObject baseInventoryObject;
    [SerializeField] private InventoryObject characterInventoryObject;
    [SerializeField] private CharacterInput characterInput;
    
    private ObjectsRef _objectsRef;
    
    private List<WeaponSpecs> _weapons = new List<WeaponSpecs>();
    
    private List<Item> _items = new List<Item>();
    
    private Aiming _rotationPoint;
    private Transform _itemsTranform;
    
    private int _keys;
    private int _money;

    public int money
    {
        get => _money;
        set => _money = value;
    }

    public int keys
    {
        get => _keys;
        set => _keys = value;
    }

    private int _activeWeapon;
    private int _totalWeapons;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _itemsTranform = transform.Find("Items");
        _objectsRef = FindFirstObjectByType<ObjectsRef>();
        _rotationPoint = GetComponentInChildren<Aiming>();


        LoadInventoryData();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterInput.inputSwitchWeapons)
        {
            if (_totalWeapons > 1)
            {
                SwitchWeapons();
            }
        }
    }
    private void SwitchWeapons()
    {
        _weapons[_activeWeapon].gameObject.SetActive(false);
        if (_activeWeapon < _totalWeapons - 1)
        {
            _activeWeapon++;
        }
        else
        {
            _activeWeapon = 0;
        }
        
        _weapons[_activeWeapon].gameObject.SetActive(true);
        
        characterInput.inputSwitchWeapons = false;
    }

    public void AddWeapon(GameObject weapon)
    {
        GameObject wp = Instantiate(weapon, _rotationPoint.transform);
        
        wp.GameObject().SetActive(false);
        
        _weapons.Add(wp.GetComponent<WeaponSpecs>());
        _totalWeapons++;
    }
    public void AddItems(Item item)
    {
        _items.Add(_objectsRef.Items[item.id].GetComponent<Item>());
    }

    public void AddCoins(int amount)
    {
        _money += amount;
    }
    
    public List<int> GetWeaponsIds()
    {
        List<int> ids = new List<int>();

        foreach (var wp in _weapons)
        {
            ids.Add(wp.id);
        }

        return ids;
    }

    public void LoadInventoryData()
    {
        foreach (var wp in characterInventoryObject.Weapons)
        {
            GameObject _wp = Instantiate(wp, _rotationPoint.transform);
            
            _weapons.Add(_wp.GetComponent<WeaponSpecs>());
        }

        _totalWeapons = _weapons.Count;
        _activeWeapon = characterInventoryObject.ActiveWeapon;

        for (int i = 0; i < _totalWeapons; i++)
        {
            if (i == _activeWeapon)
            {
                
            }
            else
            {
                _weapons[i].gameObject.SetActive(false); 
            }
        }


        foreach (var it in characterInventoryObject.Items)
        {
            _items.Add(_objectsRef.Items[it.GetComponent<Item>().id].GetComponent<Item>());
        }
        
        _keys = characterInventoryObject.Keys; 
        _money = characterInventoryObject.Money;
    }
    public void SaveInventoryData()
    {
        characterInventoryObject.Weapons.Clear(); 
        foreach (var wp in _weapons)
        {
            characterInventoryObject.Weapons.Add(_objectsRef.Weapons[wp.id]);
        }
        
        characterInventoryObject.Items.Clear();
        foreach (var it in _items)
        {
            characterInventoryObject.Items.Add(_objectsRef.Items[it.GetComponent<Item>().id]);
        }
        
        characterInventoryObject.Keys = keys;
        characterInventoryObject.ActiveWeapon = _activeWeapon;
        characterInventoryObject.Money = _money;
    }
    public void ResetInventoryData()
    {
        characterInventoryObject.Weapons = baseInventoryObject.Weapons;
        characterInventoryObject.Items = baseInventoryObject.Items;
        characterInventoryObject.ActiveWeapon = baseInventoryObject.ActiveWeapon;
        characterInventoryObject.Keys = baseInventoryObject.Keys;
        characterInventoryObject.Money = baseInventoryObject.Money;
    }
}
