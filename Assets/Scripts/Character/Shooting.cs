using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{
    [SerializeField] private CharacterMovement playerMovement;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootdelay = 0.5f;

    private Coroutine _shootCoroutine;
    private bool _inputShoot;
    private bool _canShoot;

    private Vector2 _gamepadAimInput;
    private Camera _mainCam;
    private Vector3 _mousePos;

    public Vector2 rotation = new Vector2();
    
    public float rotZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        _canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            rotation = (_mousePos - transform.position).normalized;
        }
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            rotation = _gamepadAimInput.normalized;
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

        if (_inputShoot && _canShoot)
        {
            Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            _canShoot = false;

            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }

            _shootCoroutine = StartCoroutine("ShootDelay");
        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootdelay);
        _canShoot = true;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
    }


    public void ShootInput(InputAction.CallbackContext context)
    {
        _inputShoot = context.ReadValueAsButton();
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