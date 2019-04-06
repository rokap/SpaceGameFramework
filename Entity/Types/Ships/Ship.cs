using UnityEngine;
using System.Collections;
using System;
namespace Entities
{
    public class Ship : Entity, IShip
    {

        public AI ai = new AI();
        public Computer computer;
        public float speed;
        public int acceleration;
        public int maxthrust;

        // Use this for initialization
        void Start()
        {
            computer = new Computer(this);
            computer.Boot();
        }

        // Update is called once per frame
        void Update()
        {
            ai.Update();
            computer.Update();
        }
        private void FixedUpdate()
        {
            ai.FixedUpdate();
        }
    }
}
