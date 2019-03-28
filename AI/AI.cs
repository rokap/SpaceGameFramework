using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public partial class AI
{
    public partial class Behaviour
    {
        protected Entity owner;
    }

    IBehaviour behaviour;
    public Entity target;

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

}
