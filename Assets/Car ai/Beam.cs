using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 5))
        {
            print("hit " + hit.point);
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.red);
        } else
        {
            print("miss from " + transform.position + " - " + (transform.forward * 5));
            Debug.DrawRay(transform.position, transform.forward * 5, Color.blue);
        }
    }
}
