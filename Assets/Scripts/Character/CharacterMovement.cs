using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
   

    [SerializeField] private float dodgeForce = 20f;
    [SerializeField] private float dodgeDelay = 2f;
    [SerializeField] private Animator dashAnimator;
    private bool _canDodge = true;
    
    private CharacterInput _characterInput;
    private CharacterStats _characterStats;
    [SerializeField] private LayerMask enemyLayer;

    private Animator _animator;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private Aiming _aim;
    private Camera _camera;
    private bool _isDodging = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _aim = GetComponentInChildren<Aiming>();
        _col = GetComponent<CapsuleCollider2D>();
        _characterInput = GetComponent<CharacterInput>();
        _characterStats = GetComponent<CharacterStats>();
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
        if (!_characterInput.inputDodge && !_isDodging)
        {
            _rb.linearVelocity = _characterInput.inputMovement * _characterStats.speed;
        }
        else
        {
            _isDodging = true;
        }
        
        if (_isDodging && _canDodge)
        {
            Dodge();
        }
        
        _camera.transform.position = new Vector3(_rb.position.x, _rb.position.y, _camera.transform.position.z);
    }

    private void Dodge()
    {
        _col.excludeLayers = enemyLayer;
        _characterInput.inputDodge = true;
        _animator.SetTrigger("Dodge");
        dashAnimator.SetTrigger("Dodge");
        _rb.linearVelocity = Vector2.zero;
        _rb.AddForce((_aim.rotation).normalized * dodgeForce, ForceMode2D.Impulse);
        _canDodge = false;
        _isDodging = false;
    }

    public void ResetInputDodge()
    {
        _characterInput.inputDodge = false;
        _isDodging = false;
        _col.excludeLayers -= enemyLayer;
        StartCoroutine("DodgeDelay");
    }

    private IEnumerator DodgeDelay()
    {
        yield return new WaitForSeconds(dodgeDelay);
        _isDodging = false;
        _canDodge = true;
    }
}