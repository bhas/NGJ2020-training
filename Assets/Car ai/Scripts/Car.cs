﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float breaks;
    public float handling;
    public float speed = 0;

    public void Accelerate()
    {
        speed = Mathf.Min(speed + acceleration / 1000f, maxSpeed / 10f);
    }

    public void Reverse()
    {
        speed = Mathf.Max(speed - acceleration / 1000f, -maxSpeed / 10f);
    }

    public void Break()
    {
        Deaccelerate(breaks / 1000f);
    }

    public void Deaccelerate(float rate)
    {
        if (speed > 0)
        {
            speed = Mathf.Max(speed - rate, 0);
        }
        else
        {
            speed = Mathf.Min(speed + rate, 0);
        }
    }

    public void Turn(float rate)
    {
        transform.Rotate(new Vector3(0, rate * handling, 0));
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed, Space.Self);
        Deaccelerate(0.001f);
    }
}
