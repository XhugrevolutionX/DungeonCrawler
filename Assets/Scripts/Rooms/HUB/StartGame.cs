using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Canvas loadingCanvas;


    private void Start()
    {
        loadingCanvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            loadingCanvas.enabled = true;
            SceneManager.LoadScene("GameScene");
        }
    }
}