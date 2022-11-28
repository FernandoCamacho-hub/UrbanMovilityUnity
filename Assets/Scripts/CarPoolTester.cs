using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarPoolTester : MonoBehaviour
{
    [SerializeField] private UnityEvent _generatePositions;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // _generatePositions?.Invoke();
        }
    }
}
