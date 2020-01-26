using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorData : MonoBehaviour
{
    public static SensorData Instance;
    public float[] beamDistance = new float[5];

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
