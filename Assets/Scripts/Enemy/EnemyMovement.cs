using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // public EnemyScriptableObject enemyData;
    EnemyStats enemy;
    Transform player;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<CarController>().transform;
    }


    void Update()
    {
        // move towards player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime); 
    }
}
