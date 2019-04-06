using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class gives an order to each of the AI ships when the 
/// scene is started. References to the main Ship script are held here
/// as well as the waypoints around which the ships will fly.
/// </summary>
public class AIShipController : MonoBehaviour
{

    [Header("AI Ships")]
    [Tooltip("References to ships in the scene (tag <i>Ship</i>)")]
    public List<Ship> AIShips = new List<Ship>();
    [Tooltip("References to waypoints (tag <i>Waypoint</i>)")]
    public Transform[] Waypoints;
    int shipCount = 0;

    void Start()
    {
        foreach (GameObject shipGO in GameObject.FindGameObjectsWithTag("Ship"))
        {
            AIShips.Add(shipGO.GetComponent<Ship>());
        }
        InitSquad();
    }

    private void InitSquad()
    {

        Debug.Log("INIT Squad");
        shipCount = AIShips.Count;

        // Give orders to AI ships
        for (int i = 0; i < AIShips.Count; i++)
        {

            /**
            if (i == 0)
            {
                AIShips[i].AIController.Idle();
                AIShips[i].formation.squadSize = AIShips.Count - 1;
                AIShips[i].formation.SwitchFormation(Formation.Type.VWing, AIShips[i]);
            }
            else
            {
                AIShips[i].AIController.Follow(AIShips[0].formation.availableSpots[0]);
                AIShips[0].formation.availableSpots.RemoveAt(0);
            }
            */
        }
    }

    private void Update()
    {
        if (shipCount != AIShips.Count)
        {
            InitSquad();
        }
    }
}
