using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class AI
{
    public partial class Behaviour
    {
        protected Entity owner;
    }

    IBehaviour behaviour;

    public void ChangeBehaviour(IBehaviour newBehaviour)
    {
        if (behaviour != null)
            behaviour.End();

        behaviour = newBehaviour;
        behaviour.Start();
    }

    public void Update()
    {
        if (behaviour != null) behaviour.Update();
    }

    public void FixedUpdate()
    {
        if (behaviour != null) behaviour.FixedUpdate();
    }
    

}
