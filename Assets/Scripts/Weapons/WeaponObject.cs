using System;
using System.Collections;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{

    [SerializeField] private GameObject weapon;

    private ItemDescrptions _descriptionCanvas;
    
    private Inventory _playerInventory;
    private Collider2D _collider;
    
    void Start()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _collider = GetComponent<Collider2D>();
        _descriptionCanvas = FindFirstObjectByType<ItemDescrptions>();
        _collider.enabled = false;
    }
    
    private void OnEnable()
    {
        StartCoroutine("Spawn");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInventory.AddWeapon(weapon);
            _descriptionCanvas.ShowObjectDescription(weapon, weapon.GetComponent<WeaponSpecs>().Type);
            Destroy(gameObject);
        }
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
       _collider.enabled = true;
    }
    
}
