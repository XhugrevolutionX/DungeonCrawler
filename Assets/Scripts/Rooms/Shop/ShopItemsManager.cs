using System.Collections.Generic;
using UnityEngine;

public class ShopItemsManager : MonoBehaviour
{
    private ObjectsRef _objectsRef;
    
    private List<ShopStall> _shopStalls = new List<ShopStall>();
    private List<GameObject> _shopItems = new List<GameObject>();

    public List<GameObject> ShopItems
    {
        get => _shopItems;
        set => _shopItems = value;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _objectsRef = GetComponentInParent<ObjectsRef>();
        
        foreach (ShopStall stall in GetComponentsInChildren<ShopStall>())
        {
            _shopStalls.Add(stall);
            stall.Init();
            stall.Restock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
