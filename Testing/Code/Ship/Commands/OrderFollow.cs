using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFollow : Order
{
    public OrderFollow()
    {
        Name = "Follow";
    }

    public override void UpdateState(ShipAI controller)
    {
        SteerAction.SteerTowardsTarget(controller);

        float distance = 0;
        if (controller.wayPointList[controller.nextWayPoint] != null)
            distance = Vector3.Distance(controller.wayPointList[controller.nextWayPoint].position, controller.ship.transform.position);
      
            controller.throttle = distance > 15f ? 1f : 0f;
    }
}