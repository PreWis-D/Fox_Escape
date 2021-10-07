using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RuneConteiner : MonoBehaviour
{
    public event UnityAction<float> RuneSelected;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.transform)
                TryGetRune(hit);
        }
    }

    private void TryGetRune(RaycastHit2D hit)
    {
        if (hit.transform.TryGetComponent(out Rune rune))
        {
            rune.gameObject.SetActive(false);
            RuneSelected?.Invoke(rune.ScorePoint);
        }
        else
        {
            return;
        }
    }

}
