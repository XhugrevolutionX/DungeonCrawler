using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] CharacterMovement playerMovement;
    [SerializeField] PlayerInput playerInput;

    private Vector2 _gamepadInput;
    private Camera _mainCam;
    private Vector3 _mousePos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3();
        float rotZ;
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            rotation = _mousePos - transform.position;
        }
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            rotation = _gamepadInput.normalized;
        }
        
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

    public void GamepadInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        
        if (input != Vector2.zero)
        {
            _gamepadInput = input;
        }
    }
}
