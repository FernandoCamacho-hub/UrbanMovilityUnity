using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyGameObject : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SelfDestroy();
        }
    }

    public void SelfDestroy()
    {
        CarPoolManager.Instance.Deactivate(gameObject);
    }
}
