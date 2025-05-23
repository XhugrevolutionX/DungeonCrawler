using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Game : MonoBehaviour
{
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private Canvas playerCanvas;
    [SerializeField] private GameObject player;
    
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip endSound;
    
    
    private Tilemap _groundTilemap;
    
    private TextMeshProUGUI _endText;

    public GameObject Player => player;
    public Tilemap GroundTilemap
    {
        get => _groundTilemap;
        set => _groundTilemap = value;
    }

    void Start()
    {
        _endText = endCanvas.transform.GetChild(0).Find("EndText").GetComponent<TextMeshProUGUI>();
        endCanvas.gameObject.SetActive(false);
        music.clip = backgroundMusic;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false; // Stops play mode in the editor
            #else
                Application.Quit(); // Quits the build
            #endif
        }
    }
    
    
    public void StartNewRun()
    {
        ResetPlayerData();
        SceneManager.LoadScene("GameScene");
    }
    
    public void NextLevel()
    {
        SavePlayerData();
        SceneManager.LoadScene("GameScene");
    }
    
    public void EndRun(bool victory)
    {
        player.GetComponent<PlayerInput>().enabled = false;
        player.GetComponent<Collider2D>().enabled = false;
        playerCanvas.gameObject.SetActive(false);

        music.Stop();
        
        
        if (victory)
        {
            _endText.text = "Victory";
        }
        else
        {
            _endText.text = "Defeat";
        }
        
        
        endCanvas.gameObject.SetActive(true);
        music.clip = endSound;
        music.Play();
    }

    public void WarpToHub()
    {
        ResetPlayerData();
        SceneManager.LoadScene("HubScene");
    }

    private void SavePlayerData()
    {
        player.GetComponent<CharacterStats>().SaveStatsData();
        player.GetComponent<Inventory>().SaveInventoryData();
    }

    private void ResetPlayerData()
    {
        player.GetComponent<CharacterStats>().ResetStatsData();
        player.GetComponent<Inventory>().ResetInventoryData();
    }
}
