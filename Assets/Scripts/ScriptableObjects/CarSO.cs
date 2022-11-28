using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "ScriptableObjects/Car", order = 1)]
public class CarSO : ScriptableObject
{
    public float scale;
    public GameObject prefabModel;
}