using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class RuneGameProgress : MonoBehaviour
{
    [SerializeField] private RuneGenerator _runeGenerator;
    [SerializeField] private Slider _slider;
    [SerializeField] private RuneConteiner _runeConteiner;

    private int _currentScore = 0;
    private int _targetScore = 10;

    public event UnityAction MiniGameRunesFinished;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = _currentScore;
    }

    private void OnEnable()
    {
        _runeConteiner.RuneSelected += OnScoreChange;
    }

    private void OnDisable()
    {
        _runeConteiner.RuneSelected -= OnScoreChange;
    }

    private void OnScoreChange(int scorePoint)
    {
        _currentScore += scorePoint;
        _slider.value += scorePoint;
        TryFinishGame(); 
    }

    private void TryFinishGame()
    {
        if (_currentScore == _targetScore)
        {
            MiniGameRunesFinished?.Invoke();
            _runeGenerator.gameObject.SetActive(false);
        }
    }
}
