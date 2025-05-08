using UnityEngine;

public class PoisonPuddle : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float duration = 1f;
    private float _elapsedTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemies"))
        {
            other.GetComponentInChildren<EnemiesDamage>().Hit(damage, Vector2.zero);
        }
    }
}
