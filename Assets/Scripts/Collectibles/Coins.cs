using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int value;
    private Animator _animator;
    private Coroutine _coroutine;
    
    private bool _spinning = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spinning)
        {
            
        }
        else
        {
            _spinning = true;
            _animator.SetTrigger("Spin");
            StartCoroutine("SpinDelay", UnityEngine.Random.Range(0.5f, 1f));
        }
    }

    private IEnumerator SpinDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _spinning = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Inventory>().AddCoins(value);
            
            Destroy(gameObject);
        }
    }
}
