using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FlaskGameLogic : MonoBehaviour
{
    private GameObject _currentFlask;
    private GameObject _targetFlask;
    private Vector2 _positionOfSelectedFlask;
    private Vector2 _positionOfTargetFlask;
    private Coroutine _swapPlacesInJob;
    private float _speed = 5f;

    public event UnityAction Moved;

    private IEnumerator SwapPlaces(GameObject currentFlask, GameObject targetFlask)
    {
        Cursor.lockState = CursorLockMode.Locked;
        bool isActionOccurred = false;
        Vector2 positionCurrentFlask = currentFlask.transform.position;

        Move(currentFlask, _currentFlask.transform.position, _positionOfTargetFlask);
        Move(targetFlask, _targetFlask.transform.position, _positionOfSelectedFlask);

        yield return new WaitWhile(() => positionCurrentFlask != _positionOfTargetFlask);

        Cursor.lockState = CursorLockMode.None;

        if (isActionOccurred == false)
        {
            isActionOccurred = true;
            Moved?.Invoke();
        }

        _currentFlask = ClearItemLink();
        _targetFlask = ClearItemLink();
    }

    private void StopSwapPlaces()
    {
        StopCoroutine(_swapPlacesInJob);
    }

    private void Move(GameObject flask, Vector2 currentPosition, Vector2 targetPosition)
    {
        flask.transform.position = Vector3.MoveTowards(currentPosition, targetPosition, Time.deltaTime * _speed);
    }

    private GameObject ClearItemLink()
    {
        return null;
    }

    private void TryGetFlask(RaycastHit2D hit)
    {
        if (hit.transform.TryGetComponent(out FlaskHighlighter flask))
        {
            if (_currentFlask == flask)
                _currentFlask = null;

            if (_currentFlask == null)
            {
                _currentFlask = flask.gameObject;
                _positionOfSelectedFlask = flask.transform.position;
            }
            else
            {
                _targetFlask = flask.gameObject;
                _positionOfTargetFlask = flask.transform.position;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.transform)
                TryGetFlask(hit);
            else
                return;

            if (_swapPlacesInJob != null)
                StopSwapPlaces();
        }

        if (_currentFlask != null && _targetFlask != null)
        {
            _swapPlacesInJob = StartCoroutine(SwapPlaces(_currentFlask, _targetFlask));
        }
    }
}