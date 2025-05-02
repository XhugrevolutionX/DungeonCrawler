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
        switch (health - _tempHealth)
        {
            //Damage
            case -1 :
                _animators[Mathf.FloorToInt(health / 2)].SetTrigger("halfDamage");
                break;
            case -2 :
                if(health % 2 == 0) 
                    _animators[Mathf.FloorToInt(health / 2)].SetTrigger("fullDamage");
                else
                {
                    _animators[Mathf.FloorToInt(health / 2) + 1].SetTrigger("halfDamage");
                    _animators[Mathf.FloorToInt(health / 2)].SetTrigger("halfDamage");
                }
                break;
            //Heal
            case 1 :
                if(health % 2 == 0) 
                    _animators[Mathf.FloorToInt(health / 2) - 1].SetTrigger("halfHeal");
                else
                    _animators[Mathf.FloorToInt(health / 2)].SetTrigger("halfHeal");
                break;
            case 2 :
                if(health % 2 == 0) 
                    _animators[Mathf.FloorToInt(health / 2) - 1].SetTrigger("fullHeal");
                else
                {
                    _animators[Mathf.FloorToInt(health / 2) - 1].SetTrigger("halfHeal");
                    _animators[Mathf.FloorToInt(health / 2)].SetTrigger("halfHeal");
                }
                break;
            case 3 :
                if (health % 2 == 0)
                {
                    _animators[Mathf.FloorToInt(health / 2) - 1].SetTrigger("fullHeal");
                    _animators[Mathf.FloorToInt(health / 2) - 2].SetTrigger("halfHeal");
                }
                else
                {
                    _animators[Mathf.FloorToInt(health / 2)].SetTrigger("halfHeal");
                    _animators[Mathf.FloorToInt(health / 2) - 1].SetTrigger("fullHeal");
                }
                break; 
            case 4 :
                if (health % 2 == 0)
                {
                    _animators[Mathf.FloorToInt(health / 2) - 1].SetTrigger("fullHeal");
                    _animators[Mathf.FloorToInt(health / 2) - 2].SetTrigger("fullHeal");
                }
                else
                {
                    _animators[Mathf.FloorToInt(health / 2)].SetTrigger("halfHeal");
                    _animators[Mathf.FloorToInt(health / 2) - 1].SetTrigger("fullHeal");
                    _animators[Mathf.FloorToInt(health / 2) - 2].SetTrigger("halfHeal");
                }
                break;
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