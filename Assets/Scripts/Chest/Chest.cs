using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Chest : MonoBehaviour
{
    
    private Animator _animator;
    private Canvas _canvas;
    
    private CharacterInput _characterInput;

    private bool _open = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterInput = FindFirstObjectByType<CharacterInput>();
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
                    OpenChest();
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
    }
}
