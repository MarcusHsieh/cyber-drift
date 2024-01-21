using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour, ICollectible{
    public int healthToRestore;
    public void Collect(){
        CarStats car = FindObjectOfType<CarStats>();
        car.RestoreHealth(healthToRestore);
        Destroy(gameObject);
    }
}
