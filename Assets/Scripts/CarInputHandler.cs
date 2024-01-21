using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour {
    // Components
    CarController carController;
    public Vector2 inputVector;
    //

    void Awake() {
        carController = GetComponent<CarController>();    
    }

    void Update() {
        inputVector = Vector2.zero;

        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");

        carController.SetInputVector(inputVector);
    }
}