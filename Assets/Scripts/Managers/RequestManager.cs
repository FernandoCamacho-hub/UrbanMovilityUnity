using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//3 cosas

//1. parsing de JSON
//2. Corutinas
//3. eventos de Unity (puede que quede mañana)

public class RequestManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3[]> _spawnCars;
    private string json =  "{\"carros\": [" + 
                           "{\"id\": 0, \"x\": 0, \"y\": 0, \"z\": 0}," +
                           "{\"id\": 1, \"x\": 1, \"y\": 1, \"z\": 1}," +
                           "{\"id\": 2, \"x\": 2, \"y\": 2, \"z\": 2}," +
                           "{\"id\": 3, \"x\": 3, \"y\": 3, \"z\": 3}," +
                           "{\"id\": 4, \"x\": 4, \"y\": 4, \"z\": 4}," + 
                           "{\"id\": 5, \"x\": 5, \"y\": 5, \"z\": 5}," + 
                           "{\"id\": 6, \"x\": 6, \"y\": 6, \"z\": 6}," + 
                           "{\"id\": 7, \"x\": 7, \"y\": 7, \"z\": 7}," + 
                           "{\"id\": 8, \"x\": 8, \"y\": 8, \"z\": 8}," + 
                           "{\"id\": 9, \"x\": 9, \"y\": 9, \"z\": 9}" + 
                           "]}";
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generarJSON());
        StartCoroutine(UpdateCarPositions());

    }

    void Update(){

    }


    IEnumerator generarJSON(){
        while(true){
             yield return new WaitForSeconds(0.5f);
            
                ListaCarros carros = JsonUtility.FromJson<ListaCarros>(json);
                for(int i = 0; i < carros.carros.Length; i++){
                    carros.carros[i].x = Random.Range(0f,10f);
                    carros.carros[i].y = Random.Range(0f,10f);
                    carros.carros[i].z = Random.Range(0f,10f);

                }
                json = JsonUtility.ToJson(carros);
                print(json);

        }
    }
    IEnumerator UpdateCarPositions(){
        while(true){
             yield return new WaitForSeconds(1); 
             ListaCarros carros = JsonUtility.FromJson<ListaCarros>(json);
             Vector3[] positions = new Vector3[carros.carros.Length];
             
             for(int i = 0; i < carros.carros.Length; i++)
             {
                 float x = carros.carros[i].x;
                 float y = carros.carros[i].y;
                 float z = carros.carros[i].z;
                 Vector3 pos = new Vector3(x, y, z);
                 positions[i] = pos;
             }
             _spawnCars?.Invoke(positions);
        }
    }
    

}