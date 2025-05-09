using Unity.VisualScripting;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed;
    [SerializeField] private float knockBackForce = 5;

    private ObjectsRef _objectsRef;
    private GameObject _character;
    private GameObject _firingPoint;
    private Vector2 _direction;
    private Rigidbody2D _playerRb;

    private bool _poison = false;
    
    public int Damage
    {
        get => damage;
        set => damage = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _firingPoint = GameObject.FindGameObjectWithTag("FiringPoint");
        _character = GameObject.FindGameObjectWithTag("Player");
        _playerRb = _character.GetComponent<Rigidbody2D>();
        _objectsRef = GetComponentInParent<ObjectsRef>();
        _poison = _character.GetComponent<CharacterStats>().poison;
        
        if (_character.transform.localScale.x > 0)
        {
            _direction = _firingPoint.transform.right;
        }
        else
        {
            _direction = - _firingPoint.transform.right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Walls") || other.CompareTag("Props"))
        {
            if (_poison)
            {
                Instantiate(_objectsRef.Effects[0], transform.position, Quaternion.identity, _objectsRef.transform);
            }
            Destroy(gameObject);
            
        }
        if (other.CompareTag("Enemies"))
        {
            other.gameObject.GetComponent<EnemiesDamage>().Hit(damage, _direction * knockBackForce);
                
            if (_poison)
            {
                Instantiate(_objectsRef.Effects[0], transform.position, Quaternion.identity, _objectsRef.transform);
            }
            Destroy(gameObject);
        }
    }
}
