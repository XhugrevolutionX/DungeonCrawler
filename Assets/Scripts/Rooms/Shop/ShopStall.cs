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

    private ItemDescrptions _descriptionCanvas;
    
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
        
        _descriptionCanvas = FindFirstObjectByType<ItemDescrptions>();
        
        spriteRenderer.enabled = false;
    }
    
    public void Restock()
    {
        int rnd = UnityEngine.Random.Range(0, 100);

        if (rnd < 50)
        {
            RestockWeapon();
        }
        else
        {
            RestockItem();
        }

        if (_object != null)
        {
            spriteRenderer.sprite = _object.transform.Find("Renderer").GetComponent<SpriteRenderer>().sprite;
            spriteRenderer.enabled = true;
            _manager.ShopItems.Add(_object);
        }
    }
    
    
    private void RestockWeapon()
    {
        List<int> playerWeaponsIds = _characterInventory.GetWeaponsIds();
        List<int> shopWeaponsIds = _manager.GetShopWeaponsIds();
        
        List<int> playerItemsIds = _characterInventory.GetItemsIds();
        List<int> shopItemsIds = _manager.GetShopItemsIds();

        if (playerWeaponsIds.Count + shopWeaponsIds.Count >= _objectsRef.Weapons.Length)
        {
            if (playerItemsIds.Count + shopItemsIds.Count >= _objectsRef.Items.Length)
            {
                Debug.Log("All Items and Weapons have already been spawned");
            }
            else
            {
                RestockItem();
            }
        }
        else
        {
            int rnd;
            do
            {
                rnd = Random.Range(0, _objectsRef.Weapons.Length);
                
            } while (playerWeaponsIds.Contains(rnd) || shopWeaponsIds.Contains(rnd));
            
            _object = _objectsRef.Weapons[rnd];
            _objectId = rnd;
            priceTag.text = _object.GetComponent<WeaponSpecs>().Price + " $";
            _price = _object.GetComponent<WeaponSpecs>().Price;
            _objectType = _object.GetComponent<WeaponSpecs>().Type;
        }
    }
    private void RestockItem()
    {
        
        List<int> playerItemsIds = _characterInventory.GetItemsIds();
        List<int> shopItemsIds = _manager.GetShopItemsIds();
        
        List<int> playerWeaponsIds = _characterInventory.GetWeaponsIds();
        List<int> shopWeaponsIds = _manager.GetShopWeaponsIds();

        if (playerItemsIds.Count + shopItemsIds.Count >= _objectsRef.Items.Length)
        {
            if (playerWeaponsIds.Count + shopWeaponsIds.Count >= _objectsRef.Weapons.Length)
            {
                Debug.Log("All Items and Weapons have already been spawned");
            }
            else
            {
                RestockWeapon();
            }
        }
        else
        {
            int rnd;

            do
            {
                rnd = Random.Range(0, _objectsRef.Items.Length);
                
            } while (playerItemsIds.Contains(rnd) || shopItemsIds.Contains(rnd));
            
            _object = _objectsRef.Items[rnd];
            _objectId = rnd;
            priceTag.text = _object.GetComponent<Item>().Price + " $";
            _price = _object.GetComponent<Item>().Price;
            _objectType = _object.GetComponent<Item>().Type;
        }
    }
    
    
    private void ItemBought(GameObject player)
    {
        switch (_objectType)
        {
            case 0:
                _characterInventory.AddWeapon(_objectsRef.Weapons[_objectId]);
                _descriptionCanvas.ShowObjectDescription(_object, _objectType);
                _manager.ShopItems.Remove(_object);
                break;
            case 1:
                _characterInventory.AddItems(_objectsRef.Items[_objectId].GetComponent<Item>());
                _object.GetComponent<Item>().ApplyItem(player);
                _descriptionCanvas.ShowObjectDescription(_object, _objectType);
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
