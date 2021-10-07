using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MiniGameRunesFinish : MonoBehaviour
{
    [SerializeField] private Animator _winAnimation;
    [SerializeField] private GameObject[] _activatedObject;
    [SerializeField] private RuneGameProgress _runesGame;

    private const string _stringWin = "Win";

    private void OnEnable()
    {
        _runesGame.MiniGameRunesFinished += OnChangeActiveObjects;
    }

    private void OnChangeActiveObjects()
    {
        _winAnimation.SetTrigger(_stringWin);

        for (int i = 0; i < _activatedObject.Length; i++)
        {
            if (_activatedObject[i].TryGetComponent(out ObjectActivationMarker objectActivation))
                _activatedObject[i].SetActive(true);
            else
                _activatedObject[i].SetActive(false);
        }

        _runesGame.MiniGameRunesFinished -= OnChangeActiveObjects;
    }
}
