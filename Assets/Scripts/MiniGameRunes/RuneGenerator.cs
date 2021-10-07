using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneGenerator : ObjectPool
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private GameObject[] _templates;
    [SerializeField] private float _secondsBetweenSpawn;

    private Transform[] _points;
    private float _elapsedTime = 0;

    private void Start()
    {
        _points = new Transform[_spawnPoints.childCount];

        for (int i = 0; i < _spawnPoints.childCount; i++)
            _points[i] = _spawnPoints.GetChild(i);

        for (int i = 0; i < _templates.Length; i++)
            Initialize(_templates[i]);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsBetweenSpawn && TryGetObject(out GameObject rune))
        {
                _elapsedTime = 0;

                var pointPosition = _points[Random.Range(0, _points.Length)];
                Vector3 spawnPoint = new Vector3(pointPosition.position.x, pointPosition.position.y, pointPosition.position.z);
                rune.SetActive(true);
                rune.transform.position = spawnPoint;
        }
    }
}
