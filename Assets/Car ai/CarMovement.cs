using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    public float maxTurnRate;
    private NeuralNetwork Network;

    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Car ai/network02.txt";
        //string path = "Assets/Car ai/networkCarstenSomDriver.txt";
        Network = new NeuralNetwork(path);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(Vector3.forward * speed, Space.Self);

        var dx = Input.GetAxis("Horizontal");
        //Turn(maxTurnRate * dx);

        float[] inputs = SensorData.Instance.beamDistance;
        float[] output = Network.Evaluate(inputs);
        Turn(output[0]);
        print("output: " + string.Join(", ", output));
    }

    public void Turn(float rate)
    {
        var turn = Mathf.Clamp(rate, -maxTurnRate, maxTurnRate);
        transform.Rotate(new Vector3(0, turn, 0));
        print("turn: " + string.Join(", ", turn));
    }
}
