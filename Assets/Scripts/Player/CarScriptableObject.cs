using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CarScriptableObject", menuName = "ScriptableObjects/Car", order = 0)]
public class CarScriptableObject : ScriptableObject {
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    [SerializeField]
    float driftFactor;
    public float DriftFactor { get => driftFactor; private set => driftFactor = value; }
    [SerializeField]
    float accelerationFactor;
    public float AccelerationFactor { get => accelerationFactor; private set => accelerationFactor = value; }
    [SerializeField]
    float turnFactor;
    public float TurnFactor { get => turnFactor; private set => turnFactor = value; }
    [SerializeField]
    float maxSpeed;
    public float MaxSpeed { get => maxSpeed; private set => maxSpeed = value; }


    // shield hits
    
}
