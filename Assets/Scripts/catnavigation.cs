using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class catnavigation : MonoBehaviour
{
    //Timer variable; keeps track of time
    public float timer;

    //Timer interval variable; calls things when timer = this
    public float wandertimer = 2;

    //Wandering distance limit
    public float wanderdist = 10;

    //Minimum wandering distance; added to random transforms of targets for wandering
    public float minwanderdist = 3;

    //Target vector for cat wandering around room
    private Vector3 wandertarget;

    //Whether or not cat is wandering room
    public bool wander;

    private NavMeshAgent agent;

    private float pathtarget;

    // Start is called before the first frame update
    void Start()
    {
        // Assign navmesh agent
        agent = GetComponent<NavMeshAgent>();

        //Set timer to first wander interval so cat begins wandering instantly
        //timer = wandertimer;

        //Start cat wandering around room
        wander = true;

        //Give cat starting position on entering room
        agent.SetDestination(new Vector3(-16, 0, 0));
    }

    //Boolean for if the cat has reached its current destination
    protected bool pathComplete()
    {
        pathtarget = Vector3.Distance(agent.destination, agent.transform.position);
        if (pathtarget <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        //When wandering and not en-route
        if (wander && pathComplete())
        {
            //Add to timer until it reaches the wandering interval
            timer += Time.deltaTime;
            if (timer >= wandertimer)
            {
                //Find random point around cat within wanderdistance
                Vector3 randomDirection = Random.insideUnitSphere * wanderdist;
                randomDirection += transform.position;
                //Add unit vector of displacement multiplied by the minimum wander distance to add that much to the wandering target
                randomDirection += Vector3.Normalize(randomDirection - transform.position) * minwanderdist;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDirection, out hit, wanderdist, 1))
                {
                    wandertarget = hit.position;
                }
                //Set the new target
                agent.SetDestination(wandertarget);
                //Reset the timer for next time
                timer = 0;
            }
        }
    }
}
