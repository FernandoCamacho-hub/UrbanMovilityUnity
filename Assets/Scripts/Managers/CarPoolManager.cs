using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class CarPoolManager : MonoBehaviour
{
    // singleton
    private static CarPoolManager _carPoolManager;
    [SerializeField] private GameObject _carPrefab;
    [SerializeField] private int _poolSize = 0;
    private Stack<GameObject> _pool = new Stack<GameObject>();
    public static CarPoolManager Instance
    {
        get => _carPoolManager;
        set => _carPoolManager = value;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (_carPrefab == null)
        {
            throw new Exception("Es necesario que pongas un prefab para el carro");
        }

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject clone = Instantiate(_carPrefab);
            clone.SetActive(false);
            _pool.Push(clone);
        }
    }
    
    public GameObject GetGameObject()
    {
        if (_pool.Count > 0)
        {
            return _pool.Pop();
        }

        return null;
    }
    
    public void ReturnGameObjectToPool(GameObject go)
    {
        _pool.Push(go);
    }

    public GameObject Activate(Vector3 position)
    {
        if (_pool.Count == 0)
        {
            Debug.LogError("Pool is empty");
            return null;
        }
        
        GameObject clone = _pool.Pop();
        clone.transform.position = position;
        clone.SetActive(true);
        return clone;
    }
    
    public void Deactivate(GameObject go)
    {
        go.SetActive(false);
        _pool.Push(go);
    }
}
