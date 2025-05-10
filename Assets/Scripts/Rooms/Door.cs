using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public bool switchState = false;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool _state = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (boxCollider.enabled)
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
        //For editor tests
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
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Props"))
        {
            //Destroy props if the door is closed on top of them
            if (_state)
            {
                Destructible prop = other.gameObject.GetComponent<Destructible>();
                prop.DestroyAnimation();
            }
        }
    }

    public void Open()
    {
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        tag = "Untagged";
        _state = false;
    }
    public void Close()
    {
        boxCollider.enabled = true;
        spriteRenderer.enabled = true;
        tag = "Walls";
        _state = true;
    }
}
