using System.Collections;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Vector2 _inputMovement;

    [SerializeField] private float dodgeForce = 20f;
    [SerializeField] private float dodgeDelay = 2f;
    [SerializeField] private Animator dashAnimator;
    private bool _inputDodge;
    private bool _canDodge = true;

    [SerializeField] private LayerMask enemyLayer;

    private Animator _animator;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private Shooting _aim;
    private Camera _camera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _aim = GetComponentInChildren<Shooting>();
        _col = GetComponent<CapsuleCollider2D>();
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
        if (!_inputDodge)
        {
            _rb.linearVelocity = _inputMovement * movementSpeed;
        }

        _camera.transform.position = new Vector3(_rb.position.x, _rb.position.y, _camera.transform.position.z);
    }


    public void ResetInputDodge()
    {
        _inputDodge = false;
        _col.excludeLayers -= enemyLayer;
        StartCoroutine("DodgeDelay");
    }

    public void MoveCharacter(InputAction.CallbackContext context)
    {
        _inputMovement = context.ReadValue<Vector2>();
    }

    public void DodgeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_canDodge)
            {
                _col.excludeLayers = enemyLayer;
                _inputDodge = true;
                _animator.SetTrigger("Dodge");
                dashAnimator.SetTrigger("Dodge");
                _rb.linearVelocity = Vector2.zero;
                _rb.AddForce((_aim.rotation).normalized * dodgeForce, ForceMode2D.Impulse);
                _canDodge = false;
            }
        }

    }

    private IEnumerator DodgeDelay()
    {
        yield return new WaitForSeconds(dodgeDelay);
        _canDodge = true;
    }
}