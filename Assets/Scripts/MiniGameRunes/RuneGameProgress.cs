using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RuneGameProgress : MonoBehaviour
{
    [SerializeField] private RuneGenerator _runeGenerator;
    [SerializeField] private Slider _slider;
    [SerializeField] private RuneConteiner _runeConteiner;

    public event UnityAction MiniGameRunesFinished;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
    }

    private void OnEnable()
    {
        _runeConteiner.RuneSelected += OnScoreChange;
    }

    private void OnDisable()
    {
        _runeConteiner.RuneSelected -= OnScoreChange;
    }

    private void OnScoreChange(float scorePoint)
    {
        _slider.value += scorePoint;
        TryFinishGame(); 
    }

    private void TryFinishGame()
    {
        if (_slider.value == 1)
        {
            MiniGameRunesFinished?.Invoke();
            _runeGenerator.gameObject.SetActive(false);
        }
    }
}
