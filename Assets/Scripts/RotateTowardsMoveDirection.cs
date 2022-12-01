using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsMoveDirection : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 150;
    private Vector3 _previousPosition;
    private float _maxRadiansDelta = 1;
    [SerializeField] private float _durationMovement;
    public IEnumerator SmoothMotion(Vector3 targetPosition)
    {
        float t = 0f;
        while (t <= 1.0) {
            //movement 
            t += Time.deltaTime / _durationMovement;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Mathf.SmoothStep(0, 1, t));
            
            //rotation
            Vector3 currentDirection = transform.position - _previousPosition;
            Vector3 targetDirection = Vector3.RotateTowards(transform.forward, currentDirection, _maxRadiansDelta, Time.deltaTime);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * _rotationSpeed);
            _previousPosition = transform.position;
            yield return new WaitForSeconds(0);
        }

    }
    
}
