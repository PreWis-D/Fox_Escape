using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rune : MonoBehaviour
{
    private float _scorePoint = 0.1f;

    public float ScorePoint => _scorePoint;

    public void DeactivateObjectAfterAnimation()
    {
        this.gameObject.SetActive(false);
    }
}
