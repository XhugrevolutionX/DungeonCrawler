using System;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip breakSound;
    
    
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

    public void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }

    public void DestroyAnimation()
    {
        audioSource.PlayOneShot(breakSound);
        _animator.SetTrigger("Destroy");
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemies") || other.CompareTag("PlayerBullets"))
        {
            DestroyAnimation();
        }
    }
}
