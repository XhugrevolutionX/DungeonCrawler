using System.Collections.Generic;
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
    
    private GameObject _object = null;
    
    private int _objectId;
    private int _objectType;

    private int _price;
    
    private bool _bought = false;

    public int ObjectType => _objectType;
    public int ObjectId => _objectId;

    public GameObject Object => _object;

    private void Start()
    {
      
    }

    public void Init()
    {
        _objectsRef = GetComponentInParent<ObjectsRef>();
        _manager = GetComponentInParent<ShopItemsManager>();
        
        _characterInput = FindFirstObjectByType<CharacterInput>();
        _characterInventory = FindFirstObjectByType<Inventory>();
    }
    
    public void Restock()
    {
        int rnd = Random.Range(0, 100);

        List<int> playerWeaponsIds = _characterInventory.GetWeaponsIds();
        List<int> playerItemsIds = _characterInventory.GetItemsIds();
        List<int> shopWeaponsIds = _manager.GetShopWeaponsIds();
        List<int> shopItemsIds = _manager.GetShopItemsIds();
        
        if (rnd < 50 && playerWeaponsIds.Count + shopWeaponsIds.Count < _objectsRef.Weapons.Length)
        {
            //Weapons
            do
            {
                rnd = Random.Range(0, _objectsRef.Weapons.Length);
                
            } while (playerWeaponsIds.Contains(rnd) || shopWeaponsIds.Contains(rnd));
        
            _object = _objectsRef.Weapons[rnd];
            _objectId = rnd;
            priceTag.text = _object.GetComponent<WeaponSpecs>().Price + " $";
            _price = _object.GetComponent<WeaponSpecs>().Price;
            _objectType = 0;
        }
        else if (playerItemsIds.Count + shopItemsIds.Count < _objectsRef.Items.Length)
        {
            //Items
            do
            {
                rnd = Random.Range(0, _objectsRef.Items.Length);
                
            } while (shopItemsIds.Contains(rnd) || playerItemsIds.Contains(rnd));
        
            _object = _objectsRef.Items[rnd];
            _objectId = rnd;
            priceTag.text = _object.GetComponent<Item>().Price + " $";
            _price = _object.GetComponent<Item>().Price;
            _objectType = 1;
        }
        else
        {
            Debug.Log("All Items and Weapons have already been spawned");
        }

        if (_object != null)
        {
            spriteRenderer.sprite = _object.transform.Find("Renderer").GetComponent<SpriteRenderer>().sprite;
        
            _manager.ShopItems.Add(_object);
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
