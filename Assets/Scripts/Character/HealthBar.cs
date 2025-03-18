using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private int _tempHealth;
    private Animator[] _animators;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animators = GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateHealthBar(int health)
    {
        switch (_tempHealth - health)
        {
            case 1 :
                _animators[Mathf.CeilToInt(health / 2)].SetTrigger("halfDamage");
                break;
            case 2 :
                if(health % 2 == 0) 
                    _animators[Mathf.CeilToInt(health / 2)].SetTrigger("fullDamage");
                else
                {
                    _animators[Mathf.CeilToInt(health / 2)].SetTrigger("halfDamage");
                    _animators[Mathf.CeilToInt(health / 2) - 1].SetTrigger("halfDamage");
                }
                break;
            case 0:
            default:
                break;
        }
        _tempHealth = health;
    }

    public void InitializeHealthBar(int health)
    {
        _tempHealth = health;
    }
}