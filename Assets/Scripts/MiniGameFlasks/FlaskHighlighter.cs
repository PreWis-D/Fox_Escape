using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class FlaskHighlighter : MonoBehaviour
{
    [SerializeField] private GameObject _flask;
    [SerializeField] private float _height;
    [SerializeField] private float _duration;
    [SerializeField] private int _flaskId;

    private bool _isSelected = false;
    private Rigidbody2D _rigidbody2D;
    private float _startPosition;

    public int FlaskId => _flaskId;

    private void Start()
    {
        _rigidbody2D = _flask.GetComponent<Rigidbody2D>();
        _startPosition = _flask.GetComponent<Rigidbody2D>().position.y;
    }

    private void OnMouseDown()
    {
        if (_isSelected && _flask.transform.position.y == _startPosition)
            _isSelected = !_isSelected;

        if (_isSelected == false)
            ChangePositionFlaskY(_height);
        else
            ChangePositionFlaskY(_startPosition);
    }

    private void ChangePositionFlaskY(float height)
    {
        _rigidbody2D.DOMove(Vector2.up * height, _duration, false);
        _isSelected = !_isSelected;
    }
}