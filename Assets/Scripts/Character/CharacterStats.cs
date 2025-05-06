using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [SerializeField] public float speed = 5;
    [SerializeField] public int damage = 0;
    [SerializeField] public int health = 10;
    [SerializeField] public int maxHealth = 20;
    
    [SerializeField] private NewHealthBar healthBar;
    
    private int _maxHealthLimit = 20;
    
    
    public void AddMaxHealth(int hearth)
    {
        if (maxHealth + (hearth * 2) <= _maxHealthLimit)
        {
            maxHealth += (hearth * 2) ;
        }
        else
        {
            maxHealth = _maxHealthLimit;
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void AddDamage(int damageBoost)
    {
        damage += damageBoost;
    }
    public void AddSpeed(float speedBoost)
    {
        speed += speedBoost;
    }
    
}
