using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class catnavigation : MonoBehaviour
{
    //Timer variable; keeps track of time
    public float timer;
    public float timer2;

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

    //Navigation agent for this cat
    private NavMeshAgent agent;

    //Distance from cat to current navigation target
    private float pathtargetdist;

    //Spawning target object to give cat initial target
    public GameObject spawntarget;

    public GameObject targetpoint;

    //Timer for if cat takes too long to reach target
    public float waitingtimer = 5;

    private float timeroffset;

    // Start is called before the first frame update
    void Start()
    {
        //Assign navmesh agent to cat
        agent = GetComponent<NavMeshAgent>();

        //Set timer to first wander interval so cat begins wandering instantly
        //timer = wandertimer;

        //Start cat wandering around room
        wander = true;

        //Give cat starting position on entering room
        agent.SetDestination(spawntarget.transform.position);
    }

    //Boolean for if the cat has reached its current destination
    protected bool pathComplete()
    {
        //Find distance between cat and target
        pathtargetdist = Vector3.Distance(agent.destination, agent.transform.position);
        //If cat is closer than navmeshagent stopping distance
        if (pathtargetdist <= agent.stoppingDistance)
        {
            //If the cat has reached the target or stopped moving
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
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        //When wandering and not en-route
        if ((wander && pathComplete()) || (wander && timer2 >= waitingtimer))
        {
            //Add to timer until it reaches the wandering interval
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
                
                if (Vector3.Distance(wandertarget, transform.position) > 2)
                {
                    //Set the new target
                    agent.SetDestination(wandertarget);
                    //Reset the timer for next time
                    timer = 0;
                    timer2 = 0;
                    print(wandertarget);
                    Instantiate(targetpoint, wandertarget, Quaternion.identity);
                }

            }
        }
    }
}
