using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryObject baseInventoryObject;
    [SerializeField] private InventoryObject characterInventoryObject;
    [SerializeField] private CharacterInput characterInput;
    
    private ObjectsRef _objectsRef;
    
    // private Items[] _items;
    private List<WeaponSpecs> _weapons = new List<WeaponSpecs>();
    
    private Aiming _rotationPoint;
    
    private int _keys;
    // private int _money;

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
        InitializeInventory();

        _objectsRef = FindFirstObjectByType<ObjectsRef>();
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

    private void InitializeInventory()
    {
        _rotationPoint = GetComponentInChildren<Aiming>();
        
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
        
        _keys = characterInventoryObject.Keys; 
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
        GameObject wp = Instantiate(weapon, GetComponentInChildren<Aiming>().transform);
        
        wp.GameObject().SetActive(false);
        
        _weapons.Add(wp.GetComponent<WeaponSpecs>());
        _totalWeapons++;
    }
    
    
    public void SaveInventoryData()
    {
        characterInventoryObject.Weapons.Clear(); 
        foreach (var wp in _weapons)
        {
            characterInventoryObject.Weapons.Add(_objectsRef.Weapons[wp.id]);
        }
        
        characterInventoryObject.Keys = keys;
        characterInventoryObject.ActiveWeapon = _activeWeapon;
    }
    
    public void ResetInventoryData()
    {
        characterInventoryObject.Weapons = baseInventoryObject.Weapons;
        characterInventoryObject.ActiveWeapon = baseInventoryObject.ActiveWeapon;
        characterInventoryObject.Keys = baseInventoryObject.Keys;
    }
}
