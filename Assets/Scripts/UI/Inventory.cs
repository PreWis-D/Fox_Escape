using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> _items = new List<Item>();
    [SerializeField] private ItemView _template;
    [SerializeField] private GameObject _itemConteiner;
    [SerializeField] private PlayerBag _playerBag;
    [SerializeField] private ItemCollector _itemCollector;
    [SerializeField] private CauldronConteiner _cauldronConteiner;

    private Item _currentItem;

    public event UnityAction<Item, ItemView> SelectedItem;

    private void OnEnable()
    {
        _itemCollector.ItemTaken += OnAddItem;
        _cauldronConteiner.IngridientAdded += OnDeleteItemInInventory;
    }

    private void OnDisable()
    {
        _itemCollector.ItemTaken -= OnAddItem;
        _cauldronConteiner.IngridientAdded -= OnDeleteItemInInventory;
    }

    private void OnAddItem(Item item, ItemView itemView)
    {
        _items.Add(item);
        itemView = Instantiate(_template, _itemConteiner.transform);
        itemView.SelectButtonClick += OnSelectButtonClick;
        itemView.Render(item);

    }

    private void OnSelectButtonClick(Item item, ItemView itemView)
    {
        if (_currentItem != null)
            _currentItem.UnSelect();

        SelectedItem?.Invoke(item, itemView);
        item.Select();
        _currentItem = item;
        itemView.SelectButtonClick -= OnSelectButtonClick;
    }

    private void OnDeleteItemInInventory(Item item, ItemView itemView)
    {
        itemView.gameObject.SetActive(false);
    }
}
