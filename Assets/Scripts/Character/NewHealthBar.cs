using UnityEngine;

public class NewHealthBar : MonoBehaviour
{
    private int _maxHealth;
    private int _health;
    
    private SpriteRenderer[] _spriteRenderers;

    [SerializeField] private HealthObject characterHealthObject;
    
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        
        UpdateHealthBar(characterHealthObject.Health, characterHealthObject.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(int health, int maxHealth)
    {
        _health = health;
        _maxHealth = maxHealth;
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            if (_maxHealth % 2 == 0)
            {
                if (i <= Mathf.FloorToInt(_maxHealth / 2) - 1)
                {
                    _spriteRenderers[i].enabled = true;
                }
                else
                {
                    _spriteRenderers[i].enabled = false;
                }
                
            }
            else
            {
                if (i <= Mathf.FloorToInt(_maxHealth / 2))
                {
                    _spriteRenderers[i].enabled = true;
                }
                else
                {
                    _spriteRenderers[i].enabled = false;
                }
            }

            if (health % 2 == 0)
            {
                if (i <= Mathf.FloorToInt(health / 2) - 1)
                {
                    _spriteRenderers[i].sprite = fullHeart;
                }
                else
                {
                    _spriteRenderers[i].sprite = emptyHeart;
                }
            }
            else
            {
                if (i < Mathf.FloorToInt(health / 2))
                {
                    _spriteRenderers[i].sprite = fullHeart;
                }
                else if (i == Mathf.FloorToInt(health / 2))
                {
                    _spriteRenderers[i].sprite = halfHeart;
                }
                else
                {
                    _spriteRenderers[i].sprite = emptyHeart;
                }
            }
        }
    }
}
