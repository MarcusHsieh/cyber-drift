using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehavior : DefenseBehavior
{
    int currentHits = 0;
    List<GameObject> markedEnemies;
    public override void Start(){
        // don't want to run base.Start() because don't want to destroy
        markedEnemies = new List<GameObject>();
    }

    public override void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject)){
            
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            float damageMultiplier = Mathf.Max(0, GetComponentInParent<Rigidbody2D>().velocity.magnitude);
            float totalDamage = currentDamage * damageMultiplier;
            enemy.TakeDamage(totalDamage);
            // enemy.TakeDamage(currentDamage);

            Rigidbody2D playerRigidbody = GetComponentInParent<Rigidbody2D>();
            Vector2 knockbackDirection = playerRigidbody.velocity.normalized;
            Vector2 knockbackForceVector = knockbackDirection * defenseData.KnockbackForce;

            // inc currentHits
            currentHits++;

            // apply knockback force to enemy
            enemy.ApplyKnockback(knockbackForceVector);

            // mark enemy
            markedEnemies.Add(col.gameObject);  

            // coroutine to remove enemy from list after X sec -- prevent multi hit
            StartCoroutine(RemoveEnemyAfterDelay(col.gameObject, 0.5f));

            if(currentHits >= currentHitsToDestroy)
            {
                DestroyBubble();
            }
        }
    }

    private void DestroyBubble(){

        // reset hits count for regen
        currentHits = 0;

        // start cd for regen
        StartCoroutine(RegenerateBubble());
    }

    IEnumerator RegenerateBubble(){

        // disable bubble
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(15f);

        // re-enable bubble
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
    IEnumerator RemoveEnemyAfterDelay(GameObject enemyToRemove, float delay){
        yield return new WaitForSeconds(delay);

        // remove enemy from list after delay
        markedEnemies.Remove(enemyToRemove);
    }
}
