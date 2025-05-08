using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Aiming: MonoBehaviour
{
    [SerializeField] private CharacterMovement playerMovement;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterInput characterInput;
    
    private Camera _mainCam;
    private Vector3 _mousePos;

    public Vector2 rotation = new Vector2();
    
    public float rotZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }
    
    private void Aim()
    {
        //Aiming
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            rotation = (_mousePos - transform.position).normalized;
        }
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            rotation = characterInput.gamepadAimInput.normalized;
        }

        //Switch aim side depending on the character scale
        if (playerMovement.transform.localScale.x > 0)
        {
            rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        }
        else
        {
            rotZ = Mathf.Atan2(-rotation.y, -rotation.x) * Mathf.Rad2Deg;
        }
        
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
    
}