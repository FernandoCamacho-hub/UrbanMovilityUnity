using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera[] _cameras;
    [SerializeField] private int _activeCamera;
    [SerializeField] private Camera _activeCarCamera;
    private static CameraManager _cameraManager;

    public static CameraManager Instance
    {
        get => _cameraManager;
        set => _cameraManager = value;
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
    }

    private void Start()
    {
        EnableCamera(_activeCamera);
    }

    private void EnableCamera(int cameraToEnable)
    {
        if (cameraToEnable < 0 || cameraToEnable >= _cameras.Length)
        {
            throw new System.Exception("Indice de camera a habilitar fuera de rango");
        }

        if (_cameras == null)
        {
            throw new System.Exception("No hay cameras");
        }
        
        DisableCarCamera();
        for (int i = 0; i < _cameras.Length; i++)
        {
            _cameras[i].gameObject.SetActive(i == cameraToEnable);
        }
    }

    private void DisableCarCamera()
    {
        _activeCarCamera = null;
    }
    

    public void EnableCarCamera(Camera carCamera)
    {
        print("enter enable car camera");
        _activeCarCamera = carCamera;
        _cameras[2] = _activeCarCamera;
        ChangeCamera();
    }

    public void ChangeCamera()
    {
        print("enter change camera");
        _activeCamera++;
        //asegurarnos que no excede el tamanio
        _activeCamera %= _cameras.Length;
        EnableCamera(_activeCamera);
    }
}
