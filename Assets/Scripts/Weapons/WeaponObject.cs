using System;
using System.Collections;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{

    [SerializeField] private GameObject weapon;
    [SerializeField] private Inventory playerInventory;
    private Collider2D _collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine("Spawn");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory.AddWeapon(weapon);
            Destroy(gameObject);
        }
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
       _collider.enabled = true;
    }
}
