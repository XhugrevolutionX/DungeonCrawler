using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed;
    private GameObject _character;
    private GameObject _firingPoint;
    private Vector2 _direction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _character = GameObject.FindGameObjectWithTag("Player");
        
        _direction =  _character.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * (speed * Time.deltaTime));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
        
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<CharacterHealth>().Damage(damage);
        }
    }
}
