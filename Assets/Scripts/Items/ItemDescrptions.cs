using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescrptions : MonoBehaviour
{
    [SerializeField] private float displayTime = 3f;

    private Image _background;
    private Image _image;
    private TextMeshProUGUI _text;
    
    private Coroutine _coroutine;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _background = gameObject.GetComponent<Image>();
        _image = transform.Find("ObjectImage").GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        
        _background.enabled = false;
        _image.enabled = false;
        _text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowObjectDescription(GameObject ob, int objectType)
    {
        if (_coroutine != null)
        {
            _background.enabled = false;
            _image.enabled = false;
            _text.enabled = false;
            StopCoroutine(_coroutine);
        }
        
        _image.sprite = ob.transform.Find("Renderer").GetComponent<SpriteRenderer>().sprite;
        switch (objectType)
        {
            case 0:
                _text.text = ob.GetComponent<WeaponSpecs>().WeaponDescription;
                break;
            case 1:
                _text.text = ob.GetComponent<Item>().ItemDescription;
                break;
            default:
                break;
        }
        
        _background.enabled = true;
        _image.enabled = true;
        _text.enabled = true;
        
        _coroutine = StartCoroutine(DisplayCoroutine());
    }

    private IEnumerator DisplayCoroutine()
    {
        yield return new WaitForSeconds(displayTime);
        _background.enabled = false;
        _image.enabled = false;
        _text.enabled = false;
    }
}
