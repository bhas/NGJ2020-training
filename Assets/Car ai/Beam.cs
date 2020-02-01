using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private LineRenderer line;
    private float distance;
    public int beamIndex;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        distance = line.GetPosition(1).z;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            line.startColor = Color.red;
            line.endColor = Color.red;
            SensorData.Instance.beamDistance[beamIndex] = distance - hit.distance;
        }
        else
        {
            line.startColor = Color.white;
            line.endColor = Color.white;
            SensorData.Instance.beamDistance[beamIndex] = 0;
        }
    }
}
