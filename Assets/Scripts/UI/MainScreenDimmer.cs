using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainScreenDimmer : MonoBehaviour
{
    [SerializeField] private Image _mainScreen;
    [SerializeField] private RoomChanger _roomChanger;
    [SerializeField] private float _duration;

    private void Start()
    {
        _mainScreen = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (_roomChanger != null)
            _roomChanger.WallChanged += OnRoomChanged;
    }

    private void OnDisable()
    {
        if (_roomChanger != null)
            _roomChanger.WallChanged -= OnRoomChanged;
    }

    private void OnRoomChanged()
    {
        if (_mainScreen != null)
            _mainScreen.DOFade(1, _duration).SetLoops(2, LoopType.Yoyo);
    }
}
