using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Chest : MonoBehaviour
{
    
    private Animator _animator;
    private Canvas _canvas;
    
    private CharacterInput _characterInput;
    private Inventory _characterInventory;

    private bool _open = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _canvas = GetComponentInChildren<Canvas>();
        _canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canvas.enabled = true;
            _characterInput = other.gameObject.GetComponent<CharacterInput>();
            _characterInventory = other.gameObject.GetComponent<Inventory>();
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
                _canvas.enabled = false;
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
        //Summon a Weapon or Item
        _characterInventory.keys -= 1;
    }
}
