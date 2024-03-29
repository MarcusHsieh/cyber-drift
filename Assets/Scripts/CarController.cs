using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;

    // Local variables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    // Components
    Rigidbody2D carRigidbody2D;

    void Awake() {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }
    

    void Start() {
        
    }

    void Update() {
        
    }

    void FixedUpdate() {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce(){
        // calc amt of forward in terms of direction of velocity
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        // lim speed in forward direction based on maxSpeed
        if(velocityVsUp > maxSpeed && accelerationInput > 0){
            return;
        }

        // lim speed in reverse direction based on maxSpeed / 2
        if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0){
            return;
        }

        // lim speed in any direction while accel
        if(carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0){
            return;
        }
        
        // apply drag if no accelerationInput (car stops when player lets go of gas)
        if(accelerationInput == 0){
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else{
            carRigidbody2D.drag = 0;
        }

        // create force for engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        // apply force and push car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering(){
        // limit car ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        // update rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        // apply steering by rotationg car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity(){
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector){
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

}
