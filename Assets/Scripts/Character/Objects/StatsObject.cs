using UnityEngine;

[CreateAssetMenu(fileName = "StatsObject", menuName = "CharacterObjects/StatsObject")]
public class StatsObject : ScriptableObject
{
    [SerializeField] private int health = 10;
    [SerializeField] private float speed = 5;
    [SerializeField] private int damage = 0;
    
    [SerializeField] private int maxHealth = 10;
    
    [SerializeField] private bool poison = false;

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
    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public int Damage
    {
        get => damage;
        set => damage = value;
    }

    public bool Poison
    {
        get => poison;
        set => poison = value;
    }
}
