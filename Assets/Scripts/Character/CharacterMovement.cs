using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Vector2 _inputMovement;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Shooting _aim;
    private Camera _camera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _aim = GetComponentInChildren<Shooting>();
        _camera = Camera.main;
    }

    void Update()
    {
        if (_aim.rotZ < 90 && _aim.rotZ > -90)
        {
            
        }
        else
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, 1, 1);
        }

        if (_rb.linearVelocity.magnitude > Mathf.Epsilon)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.linearVelocity = _inputMovement * movementSpeed;
        _camera.transform.position = new Vector3(_rb.position.x, _rb.position.y, _camera.transform.position.z);
    }

    public void MoveCharacter(InputAction.CallbackContext context)
    {
        _inputMovement = context.ReadValue<Vector2>();
    }
}