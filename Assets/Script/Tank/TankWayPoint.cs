using Pathfinding;
using System.Collections.Generic;
using UnityEngine;


public class TankWayPoint : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentIndex = 0;
    private AIDestinationSetter destinationSetter;

    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        if (waypoints.Length > 0)
        {
            destinationSetter.target = waypoints[0];
        }
    }

    void Update()
    {
        if (waypoints.Length == 0 || destinationSetter.target == null)
            return;

        float dist = Vector2.Distance(transform.position, destinationSetter.target.position);
        if (dist < 0.5f)
        {
            currentIndex++;
            if (currentIndex < waypoints.Length)
            {
                destinationSetter.target = waypoints[currentIndex];
            }
            else
            {
                destinationSetter.target = null; // Dừng lại
            }
        }
    }
}
