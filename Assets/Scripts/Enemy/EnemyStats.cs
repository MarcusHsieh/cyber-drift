using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    public Sprite newSprite;

    // current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    // bool
    bool isKbActive = false;
    bool isDead = false;

    Rigidbody2D rb;
    EnemyMovement enemyMovement;
    Collider2D enemyCollider;

    void Awake(){
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;

        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyCollider = GetComponent<Collider2D>();
    }

    void Update(){
        // gradually reduce velocity over time when kb active
        if(isKbActive){
            enemyMovement.enabled = false;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, enemyData.KnockbackDamping * Time.deltaTime);
        }
        // check if velocity close to zero, then stop kb
        if(rb.velocity.magnitude < 1f){
            rb.velocity = Vector2.zero;
            if(!isDead){
                enemyMovement.enabled = true;
            }
            isKbActive = false;
        }
    }
    
    public void TakeDamage(float dmg){
        currentHealth -= dmg;

        if(currentHealth <= 0){
            Kill();
        }
    }

    public void Kill(){
        // ragdoll
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = newSprite;
        enemyMovement.enabled = false;
        isDead = true;
        enemyCollider.enabled = false;
        // drop health pick-up -- spawns after 10 sec rn
        Destroy(gameObject, 10.0f); 
    }

    public void ApplyKnockback(Vector2 knockbackForce){   
        rb.velocity = Vector2.zero; 
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        
        isKbActive = true;
    }
}
