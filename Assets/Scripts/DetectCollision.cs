using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField] private string _tag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            CarPoolManager.Instance.Deactivate(gameObject);
        }
        CameraManager.Instance.ChangeCameraFromCar();
    }
}
