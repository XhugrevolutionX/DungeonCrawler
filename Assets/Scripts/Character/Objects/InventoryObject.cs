using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "CharacterObjects/InventoryObject")]
public class InventoryObject : ScriptableObject
{
    
    
    // private Items[] _items;
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    private int _activeWeapon;
    
    [SerializeField] private int keys;
    //private int _money;

    public List<GameObject> Weapons
    {
        get => weapons;
        set => weapons = value;
    }

    public int ActiveWeapon
    {
        get => _activeWeapon;
        set => _activeWeapon = value;
    }

    public int Keys
    {
        get => keys;
        set => keys = value;
    }
    
}
