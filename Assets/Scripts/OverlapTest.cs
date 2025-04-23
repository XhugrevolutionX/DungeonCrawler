using UnityEngine;

[ExecuteInEditMode]

public class OverlapTest : MonoBehaviour
{
    
    [SerializeField] private LayerMask _layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] col = Physics2D.OverlapBoxAll(transform.position, new Vector2(1,1), 0, _layerMask);

        foreach (var VARIABLE in col)
        {
            Debug.Log(VARIABLE.name);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(1,1));
    }
    
}
