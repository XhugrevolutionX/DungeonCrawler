using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("HubScene");
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false; // Stops play mode in the editor
        #else
                        Application.Quit(); // Quits the build
        #endif
    }
}
