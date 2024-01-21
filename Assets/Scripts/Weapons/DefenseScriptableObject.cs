using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DefenseScriptableObject", menuName = "ScriptableObjects/Defense", order = 0)]
public class DefenseScriptableObject : ScriptableObject {
    // base stats for defense
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    
    [SerializeField]
    float hitsToDestroy;
    public float HitsToDestroy { get => hitsToDestroy; private set => hitsToDestroy = value; }

    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    float knockbackForce;
    public float KnockbackForce { get => knockbackForce; private set => knockbackForce = value; }
}

