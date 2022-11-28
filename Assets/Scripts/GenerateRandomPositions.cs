using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GenerateRandomPositions : MonoBehaviour
{
    [SerializeField] private int _numPos;
    [SerializeField] private GameObject[] _positions;

    [SerializeField] private float _startPosition;
    [SerializeField] private float _endPosition;
    [SerializeField] private Vector3[] _initialPositions;
    private Vector3 _pos;

    private void Awake()
    {
        _positions = new GameObject[_numPos];
        _initialPositions = new Vector3[_numPos];
        for (int i = 0; i < 10; i++)
        {
            _initialPositions[i] = Vector3.zero;
        }
        
        
    }

    private void Start()
    {
        for (int i = 0; i < _numPos; i++)
        {
            _positions[i] = CarPoolManager.Instance.Activate(_initialPositions[i]);
        }
    }

    public void GeneratePosition(Vector3[] randomPos)
    {
        for (int i = 0; i < _numPos; i++)
        {
            _positions[i].transform.position = randomPos[i];
            // _pos = new Vector3(
            //     Random.Range(_startPosition, _endPosition),
            //     Random.Range(_startPosition, _endPosition),
            //     Random.Range(_startPosition, _endPosition));
            
            // _positions[i] = CarPoolManager.Instance.Activate(_pos);
        }
    }
    
    
}
