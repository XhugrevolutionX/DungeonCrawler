using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private Sprite open;
    [SerializeField] private Sprite closed;
    
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenExit()
    {
        _spriteRenderer.sprite = open;
        _collider.enabled = true;
    }

    public void CloseExit()
    {
        _spriteRenderer.sprite = closed;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterHealth>().SaveHealthData();
            other.GetComponent<Inventory>().SaveInventoryData();
            
            SceneManager.LoadScene("GameScene");
        }
        
    }
}
