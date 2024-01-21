using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : DefenseController
{
    public override void Start(){
        this.SpawnBubble();
    }
    public override void Update(){
        // just override parent
        // no cd so don't call base.Update()
    }
    private void SpawnBubble(){
        GameObject currentBubble = Instantiate(defenseData.Prefab);
        currentBubble.transform.position = transform.position;
        currentBubble.transform.parent = transform;
        // if (!currentBubble.gameObject.activeSelf)
        // {
        //     // Spawn a new bubble
        //     currentBubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        // }
    }
}
