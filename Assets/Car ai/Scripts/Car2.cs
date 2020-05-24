using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelSetup
{
    public WheelCollider collider;
    public Transform model;
}

public class Car2 : MonoBehaviour
{

    public float maxMotorTorque = 1000f;
    public float motorTorqueRate = 1000f;
    public float currentMotorTorque;
    public float maxSteeringAngle = 40f;
    public float steeringAngleRate = 100f;
    public float currentSteeringAngle;
    private float inputX;
    private float inputY;

    public WheelSetup fl;
    public WheelSetup fr;
    public WheelSetup bl;
    public WheelSetup br;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        Steer();
        Accelerate();

        // update wheel poses
        UpdateWheelPose(fr);
        UpdateWheelPose(fl);
        UpdateWheelPose(br);
        UpdateWheelPose(bl);
    }


    private void Steer()
    {
        float targetSteeringAngle = maxSteeringAngle * inputX;
        float deltaSteeringAngle = steeringAngleRate * Time.deltaTime;
        currentSteeringAngle = Step(currentSteeringAngle, targetSteeringAngle, deltaSteeringAngle);
        fl.collider.steerAngle = currentSteeringAngle;
        fr.collider.steerAngle = currentSteeringAngle;
    }

    private void Accelerate()
    {
        float targetMotorTorque = maxMotorTorque * inputY;
        float deltaMotorTorque = motorTorqueRate * Time.deltaTime;
        currentMotorTorque = Step(currentMotorTorque, targetMotorTorque, deltaMotorTorque);
        //fl.collider.motorTorque = currentMotorTorque;
        //fr.collider.motorTorque = currentMotorTorque;
        bl.collider.motorTorque = currentMotorTorque;
        br.collider.motorTorque = currentMotorTorque;
    }

    private static float Step(float currentValue, float targetValue, float rate)
    {
        if (targetValue > currentValue)
        {
            var delta = Mathf.Min(rate, targetValue - currentValue);
            currentValue += delta;
        }
        if (targetValue < currentValue)
        {
            var delta = Mathf.Min(rate, currentValue - targetValue);
            currentValue -= delta;
        }
        return currentValue;
    }

    private void UpdateWheelPose(WheelSetup wheelSetup)
    {
        wheelSetup.collider.GetWorldPose(out Vector3 pos, out Quaternion rotation);
        wheelSetup.model.position = pos;
        wheelSetup.model.rotation = rotation;
    }
}