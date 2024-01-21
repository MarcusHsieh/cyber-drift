using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for all defense

public class DefenseBehavior : MonoBehaviour
{
    public DefenseScriptableObject defenseData;

    public float destroyAfterSeconds;
    // current stats
    protected float currentDamage;
    protected float currentHitsToDestroy;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake(){
        currentDamage = defenseData.Damage;
        currentHitsToDestroy = defenseData.HitsToDestroy;
        currentCooldownDuration = defenseData.CooldownDuration;
    }

    public virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public virtual void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Enemy")){
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
    }
}
