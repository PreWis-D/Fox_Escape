using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class FlasksGameFinish : MonoBehaviour
{
    [SerializeField] private Animator _winAnimation;
    [SerializeField] private GameObject _stone;
    [SerializeField] private GameObject _flasksAfterFinishGame;
    [SerializeField] private FlaskGameLogic _gameplay;
    [SerializeField] private Transform _points;
    [SerializeField] private Transform _flasks;

    private const string _stringWin = "Win";
    private int[] _flasksId;
    private int[] _pointsId;

    private void Start()
    {
        _flasksId = CreateArray(_flasks, true);
        _pointsId = CreateArray(_points, false);
    }

    private void OnEnable()
    {
        _gameplay.Moved += OnTryFinishGame;
    }

    private void OnDisable()
    {
        _gameplay.Moved -= OnTryFinishGame;
    }

    private void OnTryFinishGame()
    {
        _flasksId = EditArray(_flasks);

        bool isfinish = CheckFinishGame();

        if (isfinish)
        {
            _winAnimation.SetTrigger(_stringWin);
            _gameplay.gameObject.SetActive(false);
            _flasks.gameObject.SetActive(false);
            _stone.gameObject.SetActive(true);
            _flasksAfterFinishGame.SetActive(true);
        }
    }

    private int[] EditArray(Transform target)
    {
        Transform[] temp = new Transform[target.childCount];
        int[] result = new int[target.childCount];

        for (int i = 0; i < target.childCount; i++)
            temp[i] = target.GetChild(i).GetComponent<Transform>();

        var filtered = temp.OrderBy(temp => temp.transform.localPosition.x).ToArray();
        
        for (int i = 0; i < result.Length; i++)
            result[i] = filtered[i].GetComponent<FlaskHighlighter>().FlaskId;

        return result;
    }

    private int[] CreateArray(Transform target, bool isFlasks)
    {
        int[] temp = new int[target.childCount];

        for (int i = 0; i < _points.childCount; i++)
        {
            if (isFlasks)
                temp[i] = _flasks.GetChild(i).GetComponent<FlaskHighlighter>().FlaskId;
            else
                temp[i] = _points.GetChild(i).GetComponent<Point>().PointId;
        }

        return temp;
    }

    private bool CheckFinishGame()
    {
        for (int i = 0; i < _pointsId.Length; i++)
        {
            if (_pointsId[i] == _flasksId[i])
                continue;
            return false;
        }

        return true;
    }
}