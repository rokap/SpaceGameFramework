using UnityEngine;

public partial class AI
{
    public partial class Behaviour
    {
        public class Fleeing : Behaviour, IBehaviour
        {
            public Fleeing(Entity owner) { this.owner = owner; }

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
