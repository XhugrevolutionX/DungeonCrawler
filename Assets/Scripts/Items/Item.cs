using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] private int healthUp;
    [SerializeField] private int price;

    public int Price => price;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            ApplyItem(other.gameObject);
        }
    }

    public void ApplyItem(GameObject player)
    {
        if (healthUp != 0)
        {
            player.GetComponent<CharacterHealth>().AddMaxHealth(healthUp);
        }
        
        player.GetComponent<Inventory>().AddItems(this);
    }
}
