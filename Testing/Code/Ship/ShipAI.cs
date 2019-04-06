using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Class performs the commands issued to the ship and provides an interface to 
/// issue commands to the ship.
/// </summary>
[System.Serializable]
public class ShipAI
{
    // The current order issued to this ship, can be null
    public Order CurrentOrder;

    // Waypoints list or target reference (some orders use only the first element)
    public List<Transform> wayPointList;
    // Index of the next waypoint in the wayPointList
    public int nextWayPoint;

    // The current position of the destination, used by each of the Orders
    public Vector3 tempDest;

    // Throttle value of engines, from 0.0 to 1.0
    public float throttle;

    // Yaw, Pitch and Roll torque values
    public Vector3 angularTorque;

    public PIDController pid_angle, pid_velocity;
    // I've experimentally determined that these parameters work quite well
    public float pid_P = 10, pid_I = 0.5f, pid_D = 0.5f;

    // Rigidbody
    public Rigidbody rBody;

    // Main Ship script with references to all ship components
    public Ship ship;

    public ShipAI(Ship s, Rigidbody r)
    {
        ship = s;
        rBody = r;

        // Initialize the PID controllers with preset parameters
        pid_angle = new PIDController(pid_P, pid_I, pid_D);
        pid_velocity = new PIDController(pid_P, pid_I, pid_D);

        wayPointList = new List<Transform>();
    }

    public void Update()
    {
        // If an order is present, perform it
        if (CurrentOrder != null)
            CurrentOrder.UpdateState(this);
        else
            throttle = 0f;
    }

    /// <summary>
    /// Called when a finishable order (move to) is completed. Includes cleanup.
    /// </summary>
    public void FinishOrder()
    {
        CurrentOrder = null;
        tempDest = Vector3.zero;
    }

    // Autopilot commands
    #region commands
    /// <summary>
    /// Commands the ship to move to a given object.
    /// </summary>
    /// <param name="destination"></param>
    public void MoveTo(Transform destination)
    {
        if (destination != null)
        {
            wayPointList.Clear();
            wayPointList.Add(destination);
            nextWayPoint = 0;

            CurrentOrder = new OrderMove();
        }
    }

    /// <summary>
    /// Commands the ship to move to a specified position.
    /// </summary>
    /// <param name="position">world position of destination</param>
    public void MoveTo(Vector3 position)
    {
        if (tempDest == Vector3.zero)
            return;

        CurrentOrder = new OrderMove();
    }

    /// <summary>
    /// Commands the ship to move through the given waypoints. Once the last one is reached,
    /// the route is restarted from the first waypoint.
    /// </summary>
    /// <param name="waypoints"></param>
    public void PatrolPath(Transform[] waypoints)
    {
        CurrentOrder = new OrderPatrol();

        wayPointList.Clear();
        wayPointList.AddRange(waypoints);
        nextWayPoint = 0;

    }

    /// <summary>
    /// Commands the ship to move randomly at low speed, roughly in the same area.
    /// </summary>
    public void Idle()
    {
        CurrentOrder = new OrderIdle();
        tempDest = ship.transform.position;

    }

    /// <summary>
    /// Commands the ship to follow a target
    /// </summary>
    public void Follow(Transform target)
    {
        if (target != null)
        {
            wayPointList.Clear();
            wayPointList.Add(target);
            nextWayPoint = 0;

            CurrentOrder = new OrderFollow();
        }

    }

    #endregion commands
}