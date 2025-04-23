using Unity.VisualScripting;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed;
    [SerializeField] private float knockBackForce = 5;
    private GameObject _character;
    private GameObject _firingPoint;
    private Vector2 _direction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _firingPoint = GameObject.FindGameObjectWithTag("FiringPoint");
        _character = GameObject.FindGameObjectWithTag("Player");
        
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Walls") || other.CompareTag("Props"))
        {
            Destroy(gameObject);
        }
        
        if (other.CompareTag("Enemies"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<EnemiesDamage>().Hit(damage, _direction * knockBackForce);
        }
    }
}
