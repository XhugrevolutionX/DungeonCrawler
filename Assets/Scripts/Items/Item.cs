using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int healthUp;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            ApplyItem(other.gameObject);
        }
    }

    private void ApplyItem(GameObject player)
    {
        if (healthUp != 0)
        {
            player.GetComponent<CharacterHealth>().AddMaxHealth(healthUp);
        }
    }
}
