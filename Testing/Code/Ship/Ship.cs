using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ties all the primary ship components together.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Ship : MonoBehaviour
{

    public Faction faction;
    public Entity owner;
    public Ship leader;
    public List<Ship> subordinates = new List<Ship>();
    public bool isLeader = false;
    public bool inSquad = false;

    #region ship components

    public float rcsThrust;
    public float mainEngineThrust;

    public Formation formation = new Formation();

    // Artificial intelligence controls
    public ShipAI AIController
    {
        get { return aiInput; }
    }
    public ShipAI aiInput;

    // Ship rigidbody physics
    public ShipPhysics physics;

    #endregion ship components

    private void Awake()
    {
        aiInput = new ShipAI(this, this.GetComponent<Rigidbody>());
        physics = new ShipPhysics(this, this.GetComponent<Rigidbody>());

        if(leader != null)
        {
            leader.subordinates.Add(this);
            leader.isLeader = true;
            inSquad = true;
            leader.inSquad = true;
        }
    }

    private void Start()
    {
        if(isLeader)
        {
            AIController.Idle();
            formation.squadSize = subordinates.Count;
            formation.SwitchFormation(Formation.Type.VWing, this);
        }
       
    }

    void Update()
    {
        physics.angularForce = new Vector3(rcsThrust, rcsThrust, rcsThrust);
        physics.linearForce = new Vector3(rcsThrust, rcsThrust, mainEngineThrust);
        aiInput.Update();
        physics.Update();
        formation.onChange();
    }

    private void FixedUpdate()
    {

        physics.FixedUpdate();
    }

    private void OnDestroy()
    {

        Debug.Log("Leader Died");
        AIShipController aiController = FindObjectOfType<AIShipController>();
        aiController.AIShips.Remove(this);
    }
}

[System.Serializable]
public class Formation
{
    public int squadSize = 0;
    public enum Type
    {
        VWing = 0,
        XWing
    }
    public enum Side
    {
        Left,
        Right
    }

    public Ship ship;
    public Type current;

    public List<Transform> availableSpots = new List<Transform>();
    private Type lastFormation;
    public bool hasChanged = false;

    public void SwitchFormation(Type type, Ship ship)
    {

        this.ship = ship;
        this.current = type;

        Transform formationParent = ship.transform.Find("Formation");
        foreach (Transform t in formationParent)
        {
            GameObject.Destroy(t.gameObject);
        }

        Debug.Log(ship.name + ": Changing Formotion To " + type);
        switch (type)
        {
            case Type.VWing:

                Side nextSide = Side.Left;

                Vector3 nextLeftPos = (Vector3.left * 15f) + (Vector3.back * 15f);
                Vector3 nextRightPos = (Vector3.right * 15f) + (Vector3.back * 15f);

                for (int i = 0; i < squadSize; i++)
                {
                    if (nextSide == Side.Left)
                    {
                        GameObject spot = new GameObject("Spot (" + (i + 1) + ")");
                        spot.transform.SetParent(formationParent);
                        spot.transform.localPosition = nextLeftPos;
                        nextLeftPos += (Vector3.left * 15f) + (Vector3.back * 15f);
                        availableSpots.Add(spot.transform);

                        nextSide = Side.Right;

                    }
                    else if (nextSide == Side.Right)
                    {
                        GameObject spot = new GameObject("Spot (" + (i + 1) + ")");
                        spot.transform.SetParent(formationParent);
                        spot.transform.localPosition = nextRightPos;
                        nextRightPos += (Vector3.right * 15f) + (Vector3.back * 15f);
                        availableSpots.Add(spot.transform);

                        nextSide = Side.Left;
                    }
                }

                break;

            case Type.XWing:

                Side nextSide1 = Side.Left;

                Vector3 nextLeftPos1 = (Vector3.left * 25f) + (Vector3.back * 25f);
                Vector3 nextRightPos1 = (Vector3.right * 25f) + (Vector3.back * 25f);

                for (int i = 0; i < squadSize; i++)
                {
                    if (nextSide1 == Side.Left)
                    {
                        GameObject spot = new GameObject("Spot (" + (i + 1) + ")");
                        spot.transform.SetParent(ship.transform);
                        spot.transform.localPosition = nextLeftPos1;
                        nextLeftPos1 += (Vector3.left * 25f) + (Vector3.back * 25f);
                        availableSpots.Add(spot.transform);

                        nextSide = Side.Right;

                    }
                    else if (nextSide1 == Side.Right)
                    {
                        GameObject spot = new GameObject("Spot (" + (i + 1) + ")");
                        spot.transform.SetParent(ship.transform);
                        spot.transform.localPosition = nextRightPos1;
                        nextRightPos1 += (Vector3.right * 25f) + (Vector3.back * 25f);
                        availableSpots.Add(spot.transform);

                        nextSide = Side.Left;
                    }
                }

                break;
        }
        foreach (Ship subordinate in ship.subordinates)
        {
            subordinate.AIController.Follow(ship.formation.availableSpots[0]);
            ship.formation.availableSpots.RemoveAt(0);
        }
      
    }

    public void onChange()
    {
        if (this.current != lastFormation)
        {
            SwitchFormation(this.current, ship);
            lastFormation = this.current;
            hasChanged = true;

            if (ship.leader != null)
            {
                if (ship.leader.formation.hasChanged)
                {
                    ship.AIController.Follow(ship.formation.availableSpots[0]);
                    ship.formation.availableSpots.RemoveAt(0);
                }
            }
        }
        else
        {
            hasChanged = false;
        }
    }
}