using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollector : MonoBehaviour
{
    // check if game obj has ICollectible interface
    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.TryGetComponent(out ICollectible collectible))        {
            // if get component, call collect method
            collectible.Collect();
        }
    }
}
