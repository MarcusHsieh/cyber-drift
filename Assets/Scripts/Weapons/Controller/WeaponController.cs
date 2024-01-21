using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for general weapons

public class WeaponController : MonoBehaviour
{

    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;


    public virtual void Start()
    {
        // set cd at start (if needed)
        currentCooldown = weaponData.CooldownDuration; 
    }


    public virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f){
            Attack();
        }
    }

    public virtual void Attack(){
        currentCooldown = weaponData.CooldownDuration;
    }
}
