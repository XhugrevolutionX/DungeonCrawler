using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private Sprite open;
    [SerializeField] private Sprite closed;

    
    private Game _game;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private Canvas _loadingCanvas;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _game = GetComponentInParent<Game>();
        _loadingCanvas = GameObject.Find("LoadingCanvas").GetComponent<Canvas>();
        _loadingCanvas.enabled = false;

        CloseExit();
    }

    public void OpenExit()
    {
        _spriteRenderer.sprite = open;
        _collider.enabled = true;
    }

    public void CloseExit()
    {
        _spriteRenderer.sprite = closed;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _loadingCanvas.enabled = true;
            _game.NextLevel();
        }
    }
}
