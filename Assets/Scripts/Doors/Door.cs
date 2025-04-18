using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool switchState = false;
    private bool _state = false;
    
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_boxCollider.enabled)
        {
            _state = true;
            Close();
        }
        else
        {
            _state = false;
            Open();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (switchState)
        {
            if (_state)
            {
                Open();
            }
            else
            {
                Close();
            }
            switchState = false;
        }
    }

    public void Open()
    {
        _boxCollider.enabled = false;
        _spriteRenderer.enabled = false;
        tag = "Untagged";
        _state = false;
    }
    public void Close()
    {
        _boxCollider.enabled = true;
        _spriteRenderer.enabled = true;
        tag = "Walls";
        _state = true;
    }
}
