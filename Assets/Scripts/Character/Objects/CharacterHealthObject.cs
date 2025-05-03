using UnityEngine;

[CreateAssetMenu(fileName = "CharacterHealthObject", menuName = "CharacterObjects/CharacterHealthObject")]
public class CharacterHealthObject : ScriptableObject
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
