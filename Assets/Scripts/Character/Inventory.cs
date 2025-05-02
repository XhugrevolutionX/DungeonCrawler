using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Inventory : MonoBehaviour
{
    [SerializeField] private CharacterInput characterInput;
    
    // private Items[] _items;
    private List<WeaponSpecs> _weapons = new List<WeaponSpecs>();
    private int _activeWeapon;
    private int _totalWeapons;
    
    private int _keys;

    public int keys
    {
        get => _keys;
        set => _keys = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var wp in GetComponentsInChildren<WeaponSpecs>())
        {
            _weapons.Add(wp);
        }
        _totalWeapons = _weapons.Count;
        _activeWeapon = 0;
        _keys = 0;

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
        GameObject wp = Instantiate(weapon, GetComponentInChildren<Aiming>().transform);
        
        wp.GameObject().SetActive(false);
        
        _weapons.Add(wp.GetComponent<WeaponSpecs>());
        _totalWeapons++;
    }
}
