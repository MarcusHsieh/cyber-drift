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

    public float despawnDistance = 20f;
    Transform player;

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
        player = FindObjectOfType<CarStats>().transform;
    }

    void Update(){
        // despawn
        if(Vector2.Distance(transform.position, player.position) >= despawnDistance){
            ReturnEnemy();
        }
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
        // if dead and not moving, then Destroy(...)
        if(isDead && !isKbActive){
            StartCoroutine(FadeOutAndDestroy());
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
        // health drop on Destroy(...)
    }

    public void ApplyKnockback(Vector2 knockbackForce){   
        rb.velocity = Vector2.zero; 
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        
        isKbActive = true;
    }


    // base dmg of car if add more weapons later (different shield types maybe)
    // private void OnCollisionStay2D(Collision2D col) {
    //     if(col.gameObject.CompareTag("Player")){
    //         CarStats car = col.gameObject.GetComponent<CarStats>();
    //         car.TakeDamage(currentDamage);
    //     }
    // }

    IEnumerator FadeOutAndDestroy()
    {
        // gradually fade out enemy
        float fadeDuration = 2f;
        float timer = 0f;
        Color startColor = GetComponent<SpriteRenderer>().color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            GetComponent<SpriteRenderer>().color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        // destroy gameObject after fading out
        Destroy(gameObject);
    }
    private void OnDestroy() {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();
    }

    // move enemy to random spawn point when called
    void ReturnEnemy(){
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
