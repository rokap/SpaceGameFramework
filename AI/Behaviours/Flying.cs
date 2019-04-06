using UnityEngine;

public partial class AI
{
    public partial class Behaviour
    {
        public class Flying : Behaviour, IBehaviour
        {
            private Vector3 position;
            public float decelDist = 10;
            Vector3 currentMovementVector;
            float velocity;
            private float targetDistance;
            private float stoppingDistance = 5;
            private bool turn;
            private float avoidanceDist = 50;

            public Flying(Entity owner, Vector3 pos) { this.owner = owner; this.position = pos; }

            public void Start()
            {
                Debug.Log("Starting " + this.GetType() + " Behaviour");

                Entities.Ship ship = (Entities.Ship)owner;
                velocity = ship.maxthrust;
            }

            public void Update()
            {
                Debug.Log(turn);
                Entities.Ship ship = (Entities.Ship)owner;

                Vector3 targetDir = position - ship.transform.position;
                // The step size is equal to speed times frame time.
                float step = ship.speed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(ship.transform.forward, targetDir, step, 0.0f);
                if (turn)
                {
                    ship.transform.rotation = Quaternion.LookRotation(newDir);
                }

                Debug.DrawRay(ship.transform.position, newDir, Color.red);
                //Debug.DrawLine(ship.transform.position, position, Color.blue);

                if (Vector3.Distance(ship.transform.position, position) <= 0.5)
                {
                    ship.computer.SetCMD(Entities.Computer.CMD.FLYTO);
                }


            }
            public void FixedUpdate()
            {
                Rigidbody rb = owner.GetComponent<Rigidbody>();
                float dist = Vector3.Distance(owner.transform.position, position);
                rb.AddRelativeForce(Vector3.forward * Mathf.Clamp(dist / ((Entities.Ship)owner).acceleration, 0, ((Entities.Ship)owner).maxthrust));

                Vector3 dir = owner.transform.TransformDirection(Vector3.forward + Vector3.right / 2);
                Debug.DrawRay(owner.transform.position, dir * avoidanceDist);
                if (Physics.Raycast(owner.transform.position, dir, avoidanceDist))
                {
                    rb.AddRelativeForce(Vector3.left* ((Entities.Ship)owner).maxthrust);
                    turn = false;
                }
                else
                    turn = true;
                dir = owner.transform.TransformDirection(Vector3.forward + Vector3.left/2);
                Debug.DrawRay(owner.transform.position, dir * avoidanceDist);
                if (Physics.Raycast(owner.transform.position, dir, avoidanceDist))
                {
                    rb.AddRelativeForce(Vector3.right * ((Entities.Ship)owner).maxthrust);
                    turn = false;
                }
                else
                    turn = true;
                dir = owner.transform.TransformDirection(Vector3.forward + Vector3.up / 2);
                Debug.DrawRay(owner.transform.position, dir * avoidanceDist);
                if (Physics.Raycast(owner.transform.position, dir, avoidanceDist))
                {
                    rb.AddRelativeForce(Vector3.down * ((Entities.Ship)owner).maxthrust);
                    turn = false;
                }
                else
                    turn = true;
                dir = owner.transform.TransformDirection(Vector3.forward + Vector3.down / 2);
                Debug.DrawRay(owner.transform.position, dir * avoidanceDist);
                if (Physics.Raycast(owner.transform.position, dir, avoidanceDist))
                {
                    rb.AddRelativeForce(Vector3.up * ((Entities.Ship)owner).maxthrust);
                    turn = false;
                }
                else
                    turn = true;
            }

            private float GetCurrentStoppingDistance(float velocity, float accelerationPossible)
            {
                return (velocity / ((Entities.Ship)owner).acceleration) * velocity * 0.5f;
            }

            public void End()
            {
                Debug.Log("Ending " + this.GetType() + " Behaviour");
            }
        }
    }
}
