using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiClient : MonoBehaviour
{
    public string neuralNetworkFile;
    public SensorData sensorData;
    private Car car;
    private NeuralNetwork network;

    void Start()
    {
        car = GetComponent<Car>();
        network = new NeuralNetwork("Assets/Car ai/" + neuralNetworkFile);
    }

    // Update is called once per frame
    void Update()
    {
        car.Accelerate();

        if (network != null)
        {
            float[] inputs = sensorData.beamDistances;
            float[] output = network.Evaluate(inputs);
            float botCont = output[0];
            botCont = Mathf.Clamp(botCont, -1f, 1f);
            car.Turn(botCont);
        }
        

        //print("===================================================================================");
        //print("beam hits: " + string.Join(", ", inputs));
        //print("botcont  : " + string.Join(", ", botCont));
        //print("usercont : " + string.Join(", ", userCont));
    }
}
