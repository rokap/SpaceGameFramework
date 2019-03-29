using UnityEngine;
using System.Collections;
using System;

public class Ship : Entity, IShip
{
   
    public AI ai = new AI();
    public Computer computer;

    // Use this for initialization
    void Start()
    {
        computer = new Computer(this);
    }

    // Update is called once per frame
    void Update()
    {
        ai.Update();
    }
}
