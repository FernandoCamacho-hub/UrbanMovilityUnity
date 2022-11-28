using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnGameObject : MonoBehaviour
{
    [SerializeField] private Camera _carCamera;

    private void OnMouseDown()
    {
        Debug.Log($"clicked on {gameObject.name}");
        EnableCamera();
    }


    void EnableCamera()
    {
        // _carCamera = gameObject.transform.GetChild(0).GetComponent<Camera>();
        _carCamera = gameObject.GetComponentInChildren<Camera>();
        CameraManager.Instance.EnableCarCamera(_carCamera);
    }
}