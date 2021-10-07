using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour
{
    public event UnityAction<Item, ItemView> ItemTaken;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.transform)
                TryGetComponent(hit);
        }
    }

    private void TryGetComponent(RaycastHit2D hit)
    {
        if (hit.transform.TryGetComponent(out Item item))
        {
            var itemView = hit.transform.GetComponent<ItemView>();
            ItemTaken?.Invoke(item, itemView);
            item.gameObject.SetActive(false);
        }
    }
}
