using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for specific melee car hit -- controller
public class MeleeController : WeaponController
{
    
    public override void Start(){
        this.Attack();
        // don't want to run base.Start() because don't want cd
    }
    public override void Update(){
        // just override parent
        // no cd so don't call base.Update()
    }

    public override void Attack(){
        //base.Attack();
        GameObject melee = Instantiate(weaponData.Prefab);
        melee.transform.position = transform.position; // assign pos to be same as this obj which is parented to player
        melee.transform.parent = transform; // therefore this spawn below this obj
        melee.transform.rotation = transform.rotation; // copies rotation of player's car
    }

}
