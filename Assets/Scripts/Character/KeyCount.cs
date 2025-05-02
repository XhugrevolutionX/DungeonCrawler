using ProceduralLevelGenerator.Unity.Examples.Common;
using TMPro;
using UnityEngine;

public class KeyCount : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Inventory _playerInventory;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _playerInventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "X" + _playerInventory.keys;
    }
}
