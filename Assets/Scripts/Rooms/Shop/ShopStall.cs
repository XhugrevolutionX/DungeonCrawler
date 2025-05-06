using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ShopStall : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI priceTag;
    [SerializeField] private Transform objectParent;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private ShopItemsManager _manager;
    
    private ObjectsRef _objectsRef;
    
    private CharacterInput _characterInput;
    private Inventory _characterInventory;
    
    private GameObject _object;
    
    private int _objectId;
    private int _objectType;

    private int _price;
    
    private bool _bought = false;

    private void Start()
    {
      
    }

    public void Init()
    {
        _objectsRef = GetComponentInParent<ObjectsRef>();
        _manager = GetComponentInParent<ShopItemsManager>();
    }
    
    public void Restock()
    {
        int rnd = Random.Range(0, 100);

        if (rnd < 50)
        {
                
            //Weapons
            rnd = Random.Range(0, _objectsRef.Weapons.Length);
        
            _object = _objectsRef.Weapons[rnd];
            _objectId = rnd;
            priceTag.text = _object.GetComponent<WeaponSpecs>().Price + " $";
            _price = _object.GetComponent<WeaponSpecs>().Price;
            _objectType = 0;
        }
        else
        {
            //Items
            rnd = Random.Range(0, _objectsRef.Items.Length);
        
            _object = _objectsRef.Items[rnd];
            _objectId = rnd;
            priceTag.text = _object.GetComponent<Item>().Price + " $";
            _price = _object.GetComponent<Item>().Price;
            _objectType = 1;
        }
        
        spriteRenderer.sprite = _object.transform.Find("Renderer").GetComponent<SpriteRenderer>().sprite;
            
        switch (_objectType)
        {
            case 0:
                _manager.ShopItems.Add(_object);
                break;
            case 1:
                _manager.ShopItems.Add(_object);
                break;
            default:
                break;
        }
    }
    
    
    private void ItemBought(GameObject player)
    {
        switch (_objectType)
        {
            case 0:
                _characterInventory.AddWeapon(_objectsRef.Weapons[_objectId]);
                _manager.ShopItems.Remove(_object);
                break;
            case 1:
                _characterInventory.AddItems(_objectsRef.Items[_objectId].GetComponent<Item>());
                _object.GetComponent<Item>().ApplyItem(player);
                _manager.ShopItems.Remove(_object);
                break;
            default:
                break;
        }
        _characterInventory.money -= _price;
        _bought = true;
        spriteRenderer.enabled = false;
        priceTag.enabled = false;
    }
    
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_bought)
            {
                _characterInput = other.gameObject.GetComponent<CharacterInput>();
                _characterInventory = other.gameObject.GetComponent<Inventory>();
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_characterInput.inputAction)
            {
                if (!_bought)
                {
                    if (_characterInventory.money >= _price)
                    {
                        ItemBought(other.gameObject);
                    }
                }
            }
        }
    }

}
