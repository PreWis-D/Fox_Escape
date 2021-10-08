using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class FlaskHighlighter : MonoBehaviour
{
    [SerializeField] private GameObject _flask;
    [SerializeField] private int _flaskId;

    private float _height = 2.5f;
    private float _duration = 0.5f;
    private bool _isSelected = false;
    private Rigidbody2D _rigidbody2D;
    private float _startPosition;

    public int FlaskId => _flaskId;

    private void Start()
    {
        _rigidbody2D = _flask.GetComponent<Rigidbody2D>();
        _startPosition = _rigidbody2D.position.y;
    }

    private void OnMouseDown()
    {
        if (_isSelected && _flask.transform.position.y == _startPosition)
            _isSelected = !_isSelected;

        if (_isSelected == false)
            LiftUp();
        else
            ReturnStartingPosition();

        _isSelected = !_isSelected;
    }

    private void LiftUp()
    {
        _rigidbody2D.DOMove(Vector2.up * _height, _duration);
    }

    private void ReturnStartingPosition()
    {
        _rigidbody2D.DOMove(Vector2.up * _startPosition, _duration);
    }
}