using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    public float maxTurnRate;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed, Space.Self);

        var dx = Input.GetAxis("Horizontal");
        Turn(maxTurnRate * dx);
    }

    public void Turn(float rate)
    {
        var turn = Mathf.Clamp(rate, -maxTurnRate, maxTurnRate);
        transform.Rotate(new Vector3(0, turn, 0));
    }
}
