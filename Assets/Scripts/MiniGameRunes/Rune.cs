using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rune : MonoBehaviour
{
    private int _scorePoint = 1;

    public int ScorePoint => _scorePoint;

    public void DeactivateObjectAfterAnimation()
    {
        this.gameObject.SetActive(false);
    }
}
