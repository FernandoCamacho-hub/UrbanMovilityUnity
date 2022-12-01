using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class CarsPositions : MonoBehaviour
{
    public void GeneratePosition(Car[] cars)
    {
        for (int i = 0; i < cars.Length; i++)
        {
            // print("print desde carPositions:");
            // print(cars[i].GetType());
            CarPoolManager.Instance.ActivateCar(cars[i]);
            CarPoolManager.Instance.UpdatePosition(cars[i]);
        }
    }
    
    // [SerializeField] private int _numPos;
    // [SerializeField] private GameObject[] _positions;
    // [SerializeField] private Vector3[] _initialPositions;

    // private void Awake()
    // {
    //     _positions = new GameObject[_numPos];
    //     _initialPositions = new Vector3[_numPos];
    //     for (int i = 0; i < 10; i++)
    //     {
    //         _initialPositions[i] = Vector3.zero;
    //     }
    // }

    // private void Start()
    // {
    //     for (int i = 0; i < _numPos; i++)
    //     {
    //         _positions[i] = CarPoolManager.Instance.Activate(_initialPositions[i]);
    //     }
    // }
    
    
    
}
