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
    // private Stack<GameObject> _pool = new Stack<GameObject>();
    private Stack<Car> _carsPool = new Stack<Car>();
    [SerializeField] private List<Car> _activeCars;
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
            // GameObject clone = Instantiate(_carPrefab);
            // clone.SetActive(false);
            // _pool.Push(clone);
           
            Car newCar = new Car();
            newCar.prefab = _carPrefab;
            newCar.prefab = Instantiate(_carPrefab);
            newCar.prefab.gameObject.SetActive(false);
            _carsPool.Push(newCar);
        }

        _activeCars = new List<Car>();
    }

    public void ActivateCar(Car car)
    {
        if (IsCarActive(car.id))
        {
            return;
        }

        car.prefab = _carsPool.Pop().prefab;
        car.prefab.gameObject.SetActive(true);
        car.prefab.gameObject.transform.position = new Vector3(car.x, car.y, car.z);
        _activeCars.Add(car);
    }

    private bool IsCarActive(int id)
    {
        for (int i = 0; i < _activeCars.Count; i++)
        {
            if (_activeCars[i].id == id)
            {
                return true;
            }
        }

        return false;
    }
    
    public void UpdatePosition(Car car)
    {
        for (int i = 0; i < _activeCars.Count; i++)
        {
            if (_activeCars[i] == null)
            {
                continue;
            }

            if (_activeCars[i].id == car.id)
            {
                _activeCars[i].x = car.x;
                _activeCars[i].y = car.y;
                _activeCars[i].z = car.z;
                Vector3 pos = new Vector3(car.x, car.y, car.z);
                // car.prefab.gameObject.transform.position = pos;
                // _activeCars[i].prefab.gameObject.transform.position = pos;
                StartCoroutine(_activeCars[i].prefab.GetComponent<RotateTowardsMoveDirection>().SmoothMotion(pos));
            }
        }
    }

    private void EliminateFromActiveCars(GameObject go)
    {
        int idToDelete = new int();
        for (int i = 0; i < _activeCars.Count; i++)
        {
            if (_activeCars[i].prefab.transform.position == go.transform.position)
            {
                idToDelete = i;
            }
        }

        _activeCars.RemoveAt(idToDelete);
    }

    public void Deactivate(GameObject go)
    {
        go.SetActive(false);
        Car car = new Car();
        car.prefab = go;
        _carsPool.Push(car);
        EliminateFromActiveCars(go);
    }
}
