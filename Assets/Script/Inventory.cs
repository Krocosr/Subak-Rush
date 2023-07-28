using Project.Data;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;


public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public bool hasItem;
    [SerializeField]
    private ItemData _itemData;
    
    public GameObject CurrentItem { get; set; }
    public ItemData ItemData { get { return _itemData; } }
    void Awake() => instance = this; 


    public void AddItem(ItemData newItem)
    {
        _itemData = newItem;
        CurrentItem = _itemData._itemPrefab[0];
    }
    public void RemoveItem()
    {
        if (_itemData._itemPrefab == null)
        {
            return;
        }
        Vector3 RandomValue = new Vector3(Random.Range(-1.5f, 1.5f), 0.5f, Random.Range(-1.5f, 1.5f));
        Instantiate(CurrentItem, transform.position + RandomValue, transform.rotation);
    }


}
