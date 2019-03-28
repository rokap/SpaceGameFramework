﻿using UnityEngine;

public partial class AI
{
    public partial class Behaviour
    {
        public class Chasing : Behaviour, IBehaviour
        {
            public Chasing(Entity owner) { this.owner = owner; }

            public void Start()
            {
                Debug.Log("Starting " + this.GetType() + " Behaviour");
            }

            public void Update()
            {
                Debug.Log("Updating " + this.GetType() + " Behaviour");
            }

            public void End()
            {
                Debug.Log("Ending " + this.GetType() + " Behaviour");
            }
        }       
    }
}