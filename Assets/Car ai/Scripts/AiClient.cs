using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiClient : MonoBehaviour
{
    public SensorData sensorData;
    private Car car;
    public NeuralNetwork network;

    void Start()
    {
        car = GetComponent<Car>();
    }

    // Update is called once per frame
    void Update()
    {
        car.Accelerate();

        if (network != null)
        {
            float[] inputs = sensorData.beamDistances;
            float[] output = network.Evaluate(inputs);
            float aiControl = output[0];
            //aiControl = Mathf.Clamp(aiControl, -1f, 1f);
            car.Turn(aiControl);
        }
        

        //print("===================================================================================");
        //print("beam hits: " + string.Join(", ", inputs));
        //print("botcont  : " + string.Join(", ", botCont));
        //print("usercont : " + string.Join(", ", userCont));
    }
}
