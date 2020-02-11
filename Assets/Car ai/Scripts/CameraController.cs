using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        var targetPoint = target.TransformPoint(0, 20, -10);
        transform.Translate((targetPoint - transform.position) * 0.2f, Space.World);
        
        targetPoint = target.TransformPoint(0, 0, 10);
        transform.LookAt(targetPoint);
    }
}
