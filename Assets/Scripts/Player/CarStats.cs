using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStats : MonoBehaviour{
    public CarScriptableObject carData;

    // local
    float currentMaxHealth;
    float currentRecovery;
    float currentDriftFactor;
    float currentAccelerationFactor;
    float currentTurnFactor;
    float currentMaxSpeed;

    void Awake()
    {
        currentMaxHealth = carData.MaxHealth;
        currentRecovery = carData.Recovery;
        currentDriftFactor = carData.DriftFactor;
        currentAccelerationFactor = carData.AccelerationFactor;
        currentTurnFactor = carData.TurnFactor;
        currentMaxSpeed = carData.MaxSpeed;
    }

}
