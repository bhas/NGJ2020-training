using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float breaks;
    public float handling;
    public float speed = 0;

    public DateTime startTime;
    public float secondsAlive;

    public void Start()
    {
        startTime = DateTime.UtcNow;
    }

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

    private void Deaccelerate(float rate)
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

    public void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        secondsAlive = (float)(DateTime.UtcNow - startTime).TotalSeconds;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed, Space.Self);
        Deaccelerate(0.002f);
    }
}
