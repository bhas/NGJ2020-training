using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    public Transform head;

    [Range(1f, 5f)]
    public float timeInterval = 2f;
    public LineRenderer activeForceLine;
    private Vector3 activeForce;

    public LineRenderer playerLine;
    private Vector3 playerTarget;
    private Vector3 playerPoint;
    [Range(0.01f, 0.1f)]
    public float playerAcc = 0.1f;

    public LineRenderer balanceLine;
    private Vector3 balanceTarget;
    private Vector3 balancePoint;
    [Range(0.01f, 0.1f)]
    public float balanceAcc = 0.1f;

    void Start()
    {
        InvokeRepeating("RandomizeTarget", 0, timeInterval);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateForces();
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        head.localPosition = new Vector3(activeForce.x * 0.1f, 0.7f, activeForce.z * 0.1f);

        var bodyPos = new Vector3(activeForce.x * 0.02f, 0, activeForce.z * 0.02f);
        transform.Translate(bodyPos, Space.World);
    }

    private void UpdateForces()
    {
        playerTarget = GetPlayerPoint();
        playerPoint = MoveLineTowardsPoint(playerPoint, playerTarget, playerAcc);
        balancePoint = MoveLineTowardsPoint(balancePoint, balanceTarget, balanceAcc);

        activeForce = balancePoint + playerPoint;
        activeForce = Vector3.ClampMagnitude(activeForce, 1.2f);

        UpdateForceLines();
    }

    private void UpdateForceLines()
    {
        if (balanceLine != null)
            balanceLine.SetPosition(1, balancePoint);
        if (activeForceLine != null)
            activeForceLine.SetPosition(1, activeForce);
        if (playerLine != null)
            playerLine.SetPosition(1, playerPoint);
    }

    private Vector3 GetPlayerPoint()
    {
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");
        return Vector3.ClampMagnitude(new Vector3(dx, 0, dy), 1);
    }

    private Vector3 MoveLineTowardsPoint(Vector3 point, Vector3 target, float acc)
    {
        var dv = target - point;
        return point + (dv * acc);
    }

    public void RandomizeTarget()
    {
        balanceTarget = Random.insideUnitCircle;
        if (balanceTarget.magnitude < 0.25f)
            balanceTarget = balanceTarget.normalized * 0.25f;
    }
}
