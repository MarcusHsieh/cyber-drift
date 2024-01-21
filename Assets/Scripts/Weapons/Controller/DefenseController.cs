using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for general defense

public class DefenseController : MonoBehaviour
{

    [Header("Defense Stats")]
    public DefenseScriptableObject defenseData;
    float currentCooldown;

    public virtual void Start()
    {
        // set cd at start (if needed)
        currentCooldown = defenseData.CooldownDuration; 
    }

    public virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f){
            Summon();
        }
    }

    public virtual void Summon(){
        currentCooldown = defenseData.CooldownDuration;
    }
}
