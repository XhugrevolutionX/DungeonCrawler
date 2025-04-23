using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    private Vector2 _inputMovement;
    private bool _inputDodge;
    private bool _inputAction;

    public Vector2 inputMovement => _inputMovement;
    public bool inputAction => _inputAction;

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
    
}
