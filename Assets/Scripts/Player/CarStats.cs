using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStats : MonoBehaviour{
    CarScriptableObject carData;

    // current stats
    [HideInInspector]
    public float currentMaxHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
    public float currentDriftFactor;
    [HideInInspector]
    public float currentAccelerationFactor;
    [HideInInspector]
    public float currentTurnFactor;
    [HideInInspector]
    public float currentMaxSpeed;
    

    // I-Frames
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    void Awake()
    {   
        // set charData before values
        // load car based on menu select
        carData = CarSelector.GetData();
        CarSelector.instance.DestroySingleton();

        currentMaxHealth = carData.MaxHealth;
        currentRecovery = carData.Recovery;
        currentDamage = carData.Damage;
        currentDriftFactor = carData.DriftFactor;
        currentAccelerationFactor = carData.AccelerationFactor;
        currentTurnFactor = carData.TurnFactor;
        currentMaxSpeed = carData.MaxSpeed;
    }

    void Update(){
        if(invincibilityTimer > 0){
            invincibilityTimer -= Time.deltaTime;
        }
        // if == 0, set to false
        else if(isInvincible){
            isInvincible = false;
        }
    }

    // exp

    public void TakeDamage(float dmg){
        if(!isInvincible){
            currentMaxHealth -= dmg;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
        }

        if(currentMaxHealth <= 0){
            Kill();
        } 
    }

    public void Kill(){
        //Destroy(gameObject);
        Debug.Log("Dead");
    }

    public void RestoreHealth(float amount){
        if(currentMaxHealth < carData.MaxHealth){
            // ensure doesn't exceed max health
            if(amount > (carData.MaxHealth - currentMaxHealth)){
                currentMaxHealth = carData.MaxHealth;
            }
            // if < max health heal x amount
            else{
                currentMaxHealth += amount;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player")){
            CarStats car = col.gameObject.GetComponent<CarStats>();
            car.TakeDamage(currentDamage);
        }
    }


}
