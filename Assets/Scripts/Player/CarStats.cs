using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarStats : MonoBehaviour{

    public CarScriptableObject carData;

    // current stats
    float currentMaxHealth;
    float currentRecovery;
    float currentDamage;
    float currentDriftFactor;
    float currentAccelerationFactor;
    float currentTurnFactor;
    float currentMaxSpeed;

    #region Current Stat Properties
    public float CurrentMaxHealth{
        get { return currentMaxHealth; }
        set {
            // if change
            if(currentMaxHealth != value){
                currentMaxHealth = value;
                if(GameManager.instance != null){
                    GameManager.instance.currentHealthDisplay.text = "Health: " + currentMaxHealth;
                }
            }
        }
    }
    public float CurrentRecovery{
        get { return currentRecovery; }
        set {
            // if change
            if(currentRecovery != value){
                currentRecovery = value;
                if(GameManager.instance != null){
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }
    public float CurrentDamage{
        get { return currentDamage; }
        set {
            // if change
            if(currentDamage != value){
                currentDamage = value;
                if(GameManager.instance != null){
                    GameManager.instance.currentDamageDisplay.text = "Damage: " + currentDamage;
                }
            }
        }
    }
    public float CurrentDriftFactor{
        get { return currentDriftFactor; }
        set {
            // if change
            if(currentDriftFactor != value){
                currentDriftFactor = value;
                if(GameManager.instance != null){
                    GameManager.instance.currentDriftDisplay.text = "Drift: " + currentDriftFactor;
                }
            }
        }
    }
    public float CurrentAccelerationFactor{
        get { return currentAccelerationFactor; }
        set {
            // if change
            if(currentAccelerationFactor != value){
                currentAccelerationFactor = value;
                if(GameManager.instance != null){
                    GameManager.instance.currentAccelDisplay.text = "Acceleration: " + currentAccelerationFactor;
                }
            }
        }
    }
    public float CurrentTurnFactor{
        get { return currentTurnFactor; }
        set {
            // if change
            if(currentTurnFactor != value){
                currentTurnFactor = value;
                if(GameManager.instance != null){
                    GameManager.instance.currentHandlingDisplay.text = "Handling: " + currentTurnFactor;
                }
            }
        }
    }
    public float CurrentMaxSpeed{
        get { return currentMaxSpeed; }
        set {
            // if change
            if(currentMaxSpeed != value){
                currentMaxSpeed = value;
                GameManager.instance.currentSpeedDisplay.text = "Speed: " + currentMaxSpeed;
            }
        }
    }
    #endregion
    

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    [Header("UI")]
    public Image healthBar;

    void Awake()
    {   
        // set charData before values
        // load car based on menu select
        carData = CarSelector.GetData();
        CarSelector.instance.DestroySingleton();

        CurrentMaxHealth = carData.MaxHealth;
        CurrentRecovery = carData.Recovery;
        CurrentDamage = carData.Damage;
        CurrentDriftFactor = carData.DriftFactor;
        CurrentAccelerationFactor = carData.AccelerationFactor;
        CurrentTurnFactor = carData.TurnFactor;
        CurrentMaxSpeed = carData.MaxSpeed;

        UpdateHealthBar();
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
            CurrentMaxHealth -= dmg;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
        }

        if(CurrentMaxHealth <= 0){
            Kill();
        } 

        UpdateHealthBar();
    }

    public void Kill(){
        if(!GameManager.instance.isGameOver){
            GameManager.instance.GameOver();
        }
    }
    void UpdateHealthBar(){
        // update health bar
        healthBar.fillAmount = CurrentMaxHealth / carData.MaxHealth;
    }

    public void RestoreHealth(float amount){
        if(CurrentMaxHealth < carData.MaxHealth){
            // ensure doesn't exceed max health
            if(amount > (carData.MaxHealth - CurrentMaxHealth)){
                CurrentMaxHealth = carData.MaxHealth;
            }
            // if < max health heal x amount
            else{
                CurrentMaxHealth += amount;
            }
            UpdateHealthBar();
        }
    }

    private void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.CompareTag("Enemy")){
            EnemyStats enemy = col.gameObject.GetComponent<EnemyStats>();
            this.TakeDamage(enemy.currentDamage);
        }
    }

    // private void OnCollisionStay2D(Collision2D col) {
    //     if(col.gameObject.CompareTag("Enemy")){
    //         CarStats car = col.gameObject.GetComponent<CarStats>();
    //         car.TakeDamage(CurrentDamage);
    //     }
    // }


}
