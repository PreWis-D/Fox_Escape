using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _itemId;
    [SerializeField] private bool _isSelected = false;

    public Sprite Icon => _icon;
    public int ItemId => _itemId;
    public bool IsSelected => _isSelected;

    public void Select()
    {
        _isSelected = true;
    }

    public void UnSelect()
    {
        _isSelected = false;
    }
}