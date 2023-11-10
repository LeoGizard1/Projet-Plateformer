using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Vector3> waypoints;
    [SerializeField] private float speed;
    private bool backward;
    private int currentWaypoint;
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var goal = startingPosition + waypoints[currentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.fixedDeltaTime);
        if ((transform.position - goal).sqrMagnitude < 0.001)
        {
            if (!backward)
            {
                currentWaypoint += 1;
                if (currentWaypoint == waypoints.Count - 1) backward = true;
            }
            else
            {
                currentWaypoint--;
                if (currentWaypoint == 0) backward = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (startingPosition == Vector3.zero) startingPosition = transform.position;
        Gizmos.color = Color.yellow;
        for (var i = 0; i < waypoints.Count - 1; i++)
            Gizmos.DrawLine(waypoints[i] + startingPosition,
                waypoints[i + 1] + startingPosition);
    }
}