using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private int _itemId;
    private Item _currentItem;
    private ItemView _currentItemView;

    public ItemView CurrentItemView => _currentItemView;
    public Item CurrentItem => _currentItem;
    public int ItemId => _itemId;

    private void OnEnable()
    {
        _inventory.SelectedItemInInventory += OnSelectItem;
    }

    private void OnDisable()
    {
        _inventory.SelectedItemInInventory -= OnSelectItem;
    }

    public void OnSelectItem(Item item, ItemView itemView)
    {
        _itemId = item.ItemId;
        _currentItem = item;
        _currentItemView = itemView;
    }
} 
