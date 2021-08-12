using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;

    private bool isFren;
    private float currentFrenForce;
    private float currentDonusAcisi;

    //[SerializeField] private float carMaxSpeed;
    [SerializeField] private float maxDonusAcisi;
    [SerializeField] private float motorTorqueForce;
    [SerializeField] private float brakeForce;

    [SerializeField] private WheelCollider onSolTekerlerkCollider;
    [SerializeField] private WheelCollider onSagTekerlerkCollider;
    [SerializeField] private WheelCollider arkaSolTekerlerkCollider;
    [SerializeField] private WheelCollider arkaSagTekerlerkCollider;

    [SerializeField] private Transform onSolTekerlekTransform;
    [SerializeField] private Transform onSagTekerlekTransform;
    [SerializeField] private Transform arkaSolTekerlekTransform;
    [SerializeField] private Transform arkaSagTekerlekTransform;

    private void FixedUpdate()
    {
        getUserInput();
        moveTheCar();
        rotateTheCar();
        rotateTheWheels();
    }

    private void rotateTheWheels()
    {
        rotateTheWheel(onSolTekerlerkCollider, onSolTekerlekTransform);
        rotateTheWheel(onSagTekerlerkCollider, onSagTekerlekTransform);
        rotateTheWheel(arkaSolTekerlerkCollider, arkaSolTekerlekTransform);
        rotateTheWheel(arkaSagTekerlerkCollider, arkaSagTekerlekTransform);
    }

    private void rotateTheWheel(WheelCollider tekerlerkCollider, Transform tekerlekTransform)
    {
        Vector3 position;
        Quaternion rotation;
        tekerlerkCollider.GetWorldPose(out position, out rotation);
        tekerlekTransform.position = position;
        tekerlekTransform.rotation = rotation;
    }

    private void rotateTheCar()
    {
        currentDonusAcisi = maxDonusAcisi * horizontalInput;
        onSolTekerlerkCollider.steerAngle = currentDonusAcisi;
        onSagTekerlerkCollider.steerAngle = currentDonusAcisi;
    }

    private void moveTheCar()
    {
        onSolTekerlerkCollider.motorTorque = verticalInput * motorTorqueForce ;
        onSagTekerlerkCollider.motorTorque = verticalInput * motorTorqueForce ;
        currentFrenForce = isFren ? brakeForce : 0f;
        if (isFren)
        {
            onSolTekerlerkCollider.brakeTorque = currentFrenForce;
            onSagTekerlerkCollider.brakeTorque = currentFrenForce;
            arkaSolTekerlerkCollider.brakeTorque = currentFrenForce;
            arkaSagTekerlerkCollider.brakeTorque = currentFrenForce;
        }else
        {
            onSolTekerlerkCollider.brakeTorque = 0f;
            onSagTekerlerkCollider.brakeTorque = 0f;
            arkaSolTekerlerkCollider.brakeTorque = 0f;
            arkaSagTekerlerkCollider.brakeTorque = 0f;
        }
    }

    private void getUserInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isFren = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetCarRotation();
        }
    }

    private void resetCarRotation()
    {
        Quaternion rotation = transform.rotation;
        rotation.z = 0f;
        rotation.x = 0f;
        transform.rotation = rotation;
    }
}
