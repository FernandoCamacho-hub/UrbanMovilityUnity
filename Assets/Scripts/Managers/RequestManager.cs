using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

//3 cosas

//1. parsing de JSON
//2. Corutinas
//3. eventos de Unity (puede que quede mañana)


public class RequestManager : MonoBehaviour
{
    [SerializeField] private string _urlRequest = "http://127.0.0.1:5000/";
    private ListaCarros _carros;
    [SerializeField] private UnityEvent<Car[]> _spawnCars;

    [SerializeField] private float _secondsBetweenRequest;
    [SerializeField] private float _timeBetweenSteps;
    private string _json;

    private void Awake()
    {
        _carros = new ListaCarros();
        _carros.steps = new Step[2];

        for (int i = 0; i < _carros.steps.Length; i++)
        {
            _carros.steps[i] = new Step();
            
            _carros.steps[i].cars = new Car[2];

            for (int j = 0; j < _carros.steps[i].cars.Length; j++)
            {
                _carros.steps[i].cars[j] = new Car();
            }
        }
    }

    void Start()
    {
        // StartCoroutine(RequestRecurrent());
        StartCoroutine(RequestSingle());
    }

    IEnumerator RequestRecurrent() {

        while(true){

            // empezamos haciendo request
            UnityWebRequest www = UnityWebRequest.Get(_urlRequest);

            // como en cualquier request este asunto es asíncrono
            yield return www.SendWebRequest();

            if(www.result != UnityWebRequest.Result.Success){
                Debug.LogError("PROBLEMA EN REQUEST");
            } 
            else
            {
                // Debug.Log("Request successful");
                _json = www.downloadHandler.text;
                //parsing de json
                _carros = JsonUtility.FromJson<ListaCarros>(_json);
            }

            yield return new WaitForSeconds(_secondsBetweenRequest);
        }
    }

    IEnumerator RequestSingle()
    {
        UnityWebRequest www = UnityWebRequest.Get(_urlRequest);
        
        yield return www.SendWebRequest();
        
        if(www.result != UnityWebRequest.Result.Success){
            Debug.LogError("PROBLEMA EN REQUEST");
        } 
        else
        {
            _json = www.downloadHandler.text;
            print(_json);
            _carros = JsonUtility.FromJson<ListaCarros>(_json);
            StartCoroutine(UpdateCarPositions());
        }
    }

    
    
    IEnumerator UpdateCarPositions()
    {
        for (int i = 0; i < _carros.steps.Length; i++)
        {
            yield return new WaitForSeconds(_timeBetweenSteps);

            Car[] cars = new Car[_carros.steps[i].cars.Length];
            // Vector3[] positions = new Vector3[_carros.steps[i].cars.Length];
            // int[] ids = new int[_carros.steps[i].cars.Length];

            for (int j = 0; j < _carros.steps[i].cars.Length; j++)
            {
                Car car = _carros.steps[i].cars[j];
                cars[j] = car;
                // print("print de request:");
                // print("id: " + car.id + " x: " + car.x + " y: " + car.y + " z: " + car.z);
                // car = _carros.steps[i].cars[i];
                // float x = _carros.steps[i].cars[j].x;
                // float y = _carros.steps[i].cars[j].y;
                // float z = _carros.steps[i].cars[j].z;
                // int id = _carros.steps[i].cars[i].id;
                // Vector3 pos = new Vector3(x, y, z);

                // positions[j] = pos;
                // ids[j] = id;
            }
            // _spawnCars?.Invoke(ids, positions);
            _spawnCars?.Invoke(cars);
        }
    }
    
}