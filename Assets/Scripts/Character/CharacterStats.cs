using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private StatsObject baseStatsObject;
    [SerializeField] private StatsObject characterStatsObject;
    
    [SerializeField] public float speed = 5;
    [SerializeField] public int damage = 0;
    [SerializeField] public int health = 10;
    [SerializeField] public int maxHealth = 20;
    [SerializeField] private NewHealthBar healthBar;
    
    private int _maxHealthLimit = 20;
    
    public bool poison = false;

    
    
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
    
    
    
    public void LoadStatsData()
    {
        health = characterStatsObject.Health;
        maxHealth = characterStatsObject.MaxHealth;
        
        damage = characterStatsObject.Damage;
        
        speed = characterStatsObject.Speed;
        
        poison = characterStatsObject.Poison;
    }

    public void SaveStatsData()
    {
        characterStatsObject.Health = health;
        characterStatsObject.MaxHealth = maxHealth;
        
        characterStatsObject.Damage = damage;
        
        characterStatsObject.Speed = speed;
        
        characterStatsObject.Poison = poison;
    }

    public void ResetStatsData()
    {
        characterStatsObject.Health = baseStatsObject.Health;
        characterStatsObject.MaxHealth = baseStatsObject.MaxHealth;
        
        characterStatsObject.Damage = baseStatsObject.Damage;
        
        characterStatsObject.Speed = baseStatsObject.Speed;
        
        characterStatsObject.Poison = baseStatsObject.Poison;
    }
    
}
