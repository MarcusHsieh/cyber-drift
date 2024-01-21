using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for all melee weapons

public class MeleeWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroyAfterSeconds;
    // current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake(){
        currentDamage = weaponData.Damage;
        currentCooldownDuration = weaponData.CooldownDuration;
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
