using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private RuneConteiner _runeConteiner;
    [SerializeField] private GameObject _objectContainer;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _objectContainer.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool[Random.Range(0, _pool.Count)];

        return result != null;
    }

    protected void OnDisableObject()
    {
        foreach (var item in _pool)
        {
            if (item.activeSelf == true)
                item.GetComponent<Rune>().DeactivateObjectAfterAnimation();
        }
    }

    public void ResetPool()
    {
        foreach (var item in _pool)
            item.SetActive(false);
    }
}