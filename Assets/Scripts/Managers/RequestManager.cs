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
    [SerializeField] private UnityEvent<Vector3[]> _spawnCars;

    [SerializeField] private float _secondsBetweenRequest;
    // private string json =  "{\"carros\": [" + 
    //                        "{\"id\": 0, \"x\": 0, \"y\": 0, \"z\": 0}," +
    //                        "{\"id\": 1, \"x\": 1, \"y\": 1, \"z\": 1}," +
    //                        "{\"id\": 2, \"x\": 2, \"y\": 2, \"z\": 2}," +
    //                        "{\"id\": 3, \"x\": 3, \"y\": 3, \"z\": 3}," +
    //                        "{\"id\": 4, \"x\": 4, \"y\": 4, \"z\": 4}," + 
    //                        "{\"id\": 5, \"x\": 5, \"y\": 5, \"z\": 5}," + 
    //                        "{\"id\": 6, \"x\": 6, \"y\": 6, \"z\": 6}," + 
    //                        "{\"id\": 7, \"x\": 7, \"y\": 7, \"z\": 7}," + 
    //                        "{\"id\": 8, \"x\": 8, \"y\": 8, \"z\": 8}," + 
    //                        "{\"id\": 9, \"x\": 9, \"y\": 9, \"z\": 9}" + 
    //                        "]}";
    private string _json;
    
    void Start()
    {
        StartCoroutine(Request());
        // StartCoroutine(generarJSON());
        StartCoroutine(UpdateCarPositions());

    }

    IEnumerator Request() {

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
                // print(_json);
                //parsing de json
                _carros = JsonUtility.FromJson<ListaCarros>(_json);
            }

            yield return new WaitForSeconds(_secondsBetweenRequest);
        }
    }

    // IEnumerator generarJSON(){
    //     while(true){
    //          yield return new WaitForSeconds(0.5f);
    //         
    //             ListaCarros carros = JsonUtility.FromJson<ListaCarros>(json);
    //             for(int i = 0; i < carros.carros.Length; i++){
    //                 carros.carros[i].x = Random.Range(0f,10f);
    //                 carros.carros[i].y = Random.Range(0f,10f);
    //                 carros.carros[i].z = Random.Range(0f,10f);
    //
    //             }
    //             json = JsonUtility.ToJson(carros);
    //             // print(json);
    //
    //     }
    // }
    
    IEnumerator UpdateCarPositions(){
        while(true){
             yield return new WaitForSeconds(1);
             Vector3[] positions = new Vector3[_carros.cars.Length];
             
             for(int i = 0; i < _carros.cars.Length; i++)
             {
                 float x = _carros.cars[i].x;
                 float y = _carros.cars[i].y;
                 float z = _carros.cars[i].z;
                 Vector3 pos = new Vector3(x, y, z);
                 positions[i] = pos;
             }
             _spawnCars?.Invoke(positions);
        }
    }
    

}