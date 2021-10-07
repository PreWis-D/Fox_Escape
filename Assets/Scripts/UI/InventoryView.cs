using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _scrollview;

    public void Open()
    {
        _scrollview.SetActive(true);
        ChangeButton(_openButton, _closeButton);
    }

    public void Close()
    {
        _scrollview.SetActive(false);
        ChangeButton(_closeButton, _openButton);
    }

    private void ChangeButton(Button fadeButton, Button appearButton)
    {
        fadeButton.gameObject.SetActive(false);
        appearButton.gameObject.SetActive(true);
    }
}
