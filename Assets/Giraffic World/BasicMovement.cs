using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var dx = Input.GetAxisRaw("Horizontal");
        var dy = Input.GetAxisRaw("Vertical");

        transform.LookAt(new Vector3(dx * 1000, 0, dy * 1000), Vector3.up);
        if (dx != 0 || dy != 0)
        {
            transform.Translate(Vector3.forward * 0.1f, Space.Self);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
