using UnityEngine;

public class NewHealthBar : MonoBehaviour
{
    
    private SpriteRenderer[] _spriteRenderers;

    [SerializeField] private StatsObject characteStatsObject;
    
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        
        UpdateHealthBar(characteStatsObject.Health, characteStatsObject.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(int health, int maxHealth)
    {
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            if (maxHealth % 2 == 0)
            {
                if (i <= Mathf.FloorToInt(maxHealth / 2) - 1)
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
                if (i <= Mathf.FloorToInt(maxHealth / 2))
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
