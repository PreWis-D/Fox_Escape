using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _selectedItem;

    private Item _item;

    public event UnityAction<Item, ItemView> SelectButtonClick;

    private void OnEnable()
    {
        _selectedItem.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _selectedItem.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Item item)
    {
        _item = item;
        _icon.sprite = item.Icon;
    }

    private void OnButtonClick()
    {
        SelectButtonClick?.Invoke(_item, this);
    }
}