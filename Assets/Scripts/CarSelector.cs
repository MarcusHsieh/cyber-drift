using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    public static CarSelector instance;
    public CarScriptableObject carData;
    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Debug.LogWarning("EXTRA" + this + " DELETED");
            Destroy(gameObject);
        }
    }
    public static CarScriptableObject GetData(){
        return instance.carData;
    }


    public void SelectCar(CarScriptableObject car){
        carData = car;
    }
    public void DestroySingleton(){
        instance = null;
        Destroy(gameObject);
    }
}
