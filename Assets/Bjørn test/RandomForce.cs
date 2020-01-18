using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    [Range(1f, 5f)]
    public float timeInterval = 2f;
    [Range(0.01f, 0.1f)]
    public float forceAcceleration = 0.1f; 
    public LineRenderer force;
    public LineRenderer target;
    public LineRenderer control;


    void Start()
    {
        InvokeRepeating("RandomizeTarget", 0, timeInterval);
    }

    // Update is called once per frame
    void Update()
    {
        var dv = target.GetPosition(1) - force.GetPosition(1);
        var newPos = force.GetPosition(1) + (dv * forceAcceleration);
        force.SetPosition(1, newPos);

        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");
        var dir = new Vector3(dx, 0, dy);
        if (dir.magnitude > 1)
        {
            dir = dir.normalized;
        }
        control.SetPosition(1, dir);
    }

    public void RandomizeTarget()
    {
        var targetVector = Random.insideUnitCircle;
        var linePos = new Vector3(targetVector.x, 0, targetVector.y);
        target.SetPosition(1, linePos);
    }
}
