using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for specific melee car hit -- behavior
public class MeleeBehavior : MeleeWeaponBehavior
{
    List<GameObject> markedEnemies;
    

    public override void Start()
    {
        // don't want to run base.Start() because don't want to destroy
        markedEnemies = new List<GameObject>();
    }

    // damage * velocity multiplier
    public override void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject)){
            
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            
            Rigidbody2D playerRigidbody = GetComponentInParent<Rigidbody2D>();
            Vector2 knockbackDirection = playerRigidbody.velocity.normalized;
            Vector2 knockbackForceVector = knockbackDirection * weaponData.KnockbackForce;

            // apply knockback force to enemy
            enemy.ApplyKnockback(knockbackForceVector);

            // mark enemy
            markedEnemies.Add(col.gameObject);  

            // coroutine to remove enemy from list after X sec -- prevent multi hit
            StartCoroutine(RemoveEnemyAfterDelay(col.gameObject, 1f));
        }
    }

    IEnumerator RemoveEnemyAfterDelay(GameObject enemyToRemove, float delay)
    {
        yield return new WaitForSeconds(delay);

        // remove enemy from list after delay
        markedEnemies.Remove(enemyToRemove);
    }
}
