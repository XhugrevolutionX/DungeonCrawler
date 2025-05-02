using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    private Vector2 _inputMovement;
    private bool _inputDodge;
    private bool _inputAction;
    private bool _inputShoot;
    private bool _inputSwitchWeapons;
    private Vector2 _gamepadAimInput;

    public Vector2 gamepadAimInput => _gamepadAimInput;
    public Vector2 inputMovement => _inputMovement;
    public bool inputAction => _inputAction;
    public bool inputShoot => _inputShoot;

    public bool inputSwitchWeapons
    {
        get => _inputSwitchWeapons;
        set => _inputSwitchWeapons = value;
    }
    public bool inputDodge
    {
        get => _inputDodge;
        set => _inputDodge = value;
    }
    
    public void MoveCharacter(InputAction.CallbackContext context)
    {
        _inputMovement = context.ReadValue<Vector2>();
    }
    
    public void ContexAction(InputAction.CallbackContext context)
    {
        _inputAction = context.ReadValueAsButton();
    }
    
    public void DodgeInput(InputAction.CallbackContext context)
    {
        _inputDodge = context.ReadValueAsButton();
    }
    
    public void ShootInput(InputAction.CallbackContext context)
    {
        _inputShoot = context.ReadValueAsButton();
    } 
    public void SwitchInput(InputAction.CallbackContext context)
    {
        _inputSwitchWeapons = context.ReadValueAsButton();
    }
    
    public void GamepadAimInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (input != Vector2.zero)
        {
            _gamepadAimInput = input;
        }
    }
}
