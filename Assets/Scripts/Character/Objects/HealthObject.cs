using UnityEngine;

[CreateAssetMenu(fileName = "HealthObject", menuName = "CharacterObjects/HealthObject")]
public class HealthObject : ScriptableObject
{
    [SerializeField] private int health = 10;
    
    [SerializeField] private int maxHealth = 10;

    public int Health
    {
        get => health;
        set => health = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
}
