using System;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] private int price;
    
    [SerializeField] private String itemDescription;
    
    [SerializeField] private int healthUp = 0;
    [SerializeField] private int damageUp = 0;
    [SerializeField] private float speedUp = 0f;
    [SerializeField] private bool hasPoison = false;
    
    private ItemDescrptions _descriptionCanvas;
    
    private int _objectType = 1;
    
    
    public int Type => _objectType;
    public string ItemDescription => itemDescription;
    public int Price => price;
    
    private void Start()
    {
        _descriptionCanvas = FindFirstObjectByType<ItemDescrptions>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            ApplyItem(other.gameObject);
            _descriptionCanvas.ShowObjectDescription(gameObject, _objectType);
            Destroy(gameObject);
        }
    }

    public void ApplyItem(GameObject player)
    {
        if (healthUp != 0)
        {
            player.GetComponent<CharacterStats>().AddMaxHealth(healthUp);
        }
        
        if (damageUp != 0)
        {
            player.GetComponent<CharacterStats>().AddDamage(damageUp);
        }  
        
        if (speedUp != 0)
        {
            player.GetComponent<CharacterStats>().AddSpeed(speedUp);
        }
        
        player.GetComponent<CharacterStats>().poison = hasPoison;
        
        player.GetComponent<Inventory>().AddItems(this);
    }
}
