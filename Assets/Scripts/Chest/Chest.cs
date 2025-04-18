using System;
using UnityEngine;

public class Chest : MonoBehaviour
{
    
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        _animator.SetTrigger("opened");
        //Summon a Weapon or Item
    }
}
