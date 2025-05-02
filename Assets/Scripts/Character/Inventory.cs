using JetBrains.Annotations;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // private Items[] _items;
    // private Weapons[] _weapons;
    private int _keys;

    public int keys
    {
        get => _keys;
        set => _keys = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _keys = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
