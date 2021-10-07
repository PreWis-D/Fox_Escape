using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CauldronConteiner : MonoBehaviour
{
    [SerializeField] private PlayerBag _playerBag;

    private int _currentCountItems = 0;
    private int _targetItemsForWin = 4;
    private bool _isItemsNeededForCouldron => _playerBag.ItemId > 0 && _playerBag.ItemId < 5;

    public event UnityAction PoisoWasBrewed;
    public event UnityAction<Item, ItemView> IngridientAdded;

    private void OnMouseDown()
    {
        if (_isItemsNeededForCouldron)
        {
            Item item = _playerBag.CurrentItem;
            ItemView itemView = _playerBag.CurrentItemView;
            _currentCountItems++;
            IngridientAdded(item, itemView);
        }

        if (_currentCountItems == _targetItemsForWin)
        {
            PoisoWasBrewed?.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
