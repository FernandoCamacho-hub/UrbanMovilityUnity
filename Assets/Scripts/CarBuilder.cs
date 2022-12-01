using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarBuilder : MonoBehaviour
{

    [SerializeField] private CarSO[] _data;
    private GameObject _carritoInterno;
    // private void Awake()
    // {
    //     UpdateCar();
    // }

    private void OnEnable()
    {
        UpdateCar();
        //set the camera of the cars to inactive
    }

    private void UpdateCar()
    {
        CarSO curCarSo = _data[Random.Range(0, _data.Length)];
        
        if (_carritoInterno != null)
        {
            Destroy(_carritoInterno);    
        }
        
        _carritoInterno = Instantiate(curCarSo.prefabModel, transform.position, transform.rotation, transform);

        _carritoInterno.transform.localScale = new Vector3(curCarSo.scale, curCarSo.scale, curCarSo.scale);
    }

    private void OnDisable()
    {
    }
}