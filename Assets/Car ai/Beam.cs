using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public int beamIndex;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
        {
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.red);
            SensorData.Instance.beamDistance[beamIndex] = hit.distance;
        } else
        {
            Debug.DrawRay(transform.position, transform.forward * 5, Color.blue);
            SensorData.Instance.beamDistance[beamIndex] = 5;
        }
    }
}
