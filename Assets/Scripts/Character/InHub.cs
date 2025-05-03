using UnityEngine;
using UnityEngine.SceneManagement;

public class InHub : MonoBehaviour
{

    private WeaponSpecs _weapon;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _weapon = GetComponentInChildren<WeaponSpecs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "HubScene")
        {
            _weapon.gameObject.SetActive(false);
        }
        else
        {
            _weapon.gameObject.SetActive(true);
        }
    }
}
