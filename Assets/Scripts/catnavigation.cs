using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class catnavigation : MonoBehaviour
{
    //Timer variable; keeps track of time
    public float timerwander;
    public float timerwaiting;
    public float timertargeting;

    //Basic speed of cat between spawning and beginning navigation
    public float speed = 2;
    
    //Timer interval variable; calls things when timer = this
    public float wandertimer = 2;

    public float targettimer = 6;

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

    public bool targeting;

    public bool spawning;

    public GameObject currenttarget;

    public GameObject prenavtarget;

    // Start is called before the first frame update
    void Start()
    {
        //Get level-entry target from spawner
        prenavtarget = GameObject.Find("Spawner").GetComponent<catspawner>().entertarget;
        
        //Assign navmesh agent to cat
        agent = GetComponent<NavMeshAgent>();

        //Set timer to first wander interval so cat begins wandering instantly
        //timerwander = wandertimer;

        //Start cat wandering around room
        wander = false;

        //Start cat targeting an object (doesn't work if set to true)
        targeting = false;

        //Set cat to spawning, to give it spawn scripting
        spawning = true;

        //Set cat to face the pre-navigation target object, for entering level
        transform.LookAt(prenavtarget.transform);
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

    //Calls whenever a cat collides with a collider
    void OnTriggerEnter(Collider other)
    {
        //Find object responsible for collision
        GameObject collision = other.gameObject;

        //Check if object is the target, and if this cat is the one targeting the object
        if (collision == currenttarget && targeting == true)
        {
            //Destroy the target
            Destroy(currenttarget);

            //Reset the targeting timer
            timertargeting = 0;
            timerwander = 0;
            timerwaiting = 0;

            //Reset the booleans
            targeting = false;
            wander = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Various timer updates
        timerwander += Time.deltaTime;
        timerwaiting += Time.deltaTime;
        timertargeting += Time.deltaTime;

        if (spawning)
        {
            //print(Vector3.Distance(transform.position, prenavtarget.transform.position));
            if (Vector3.Distance(transform.position, prenavtarget.transform.position) < 0.2)
            {
                //Set booleans so cat begins wandering
                spawning = false;
                wander = true;

                //Set timers to 0 so things make sense
                timerwander = 0;
                timerwaiting = 0;
                timertargeting = 0;

                //Assign spawntarget for initial navmesh pathfinding
                spawntarget = GameObject.Find("SpawnTarget");

                //Give cat starting position on entering room
                agent.SetDestination(spawntarget.transform.position);
            }
            else
            {
                transform.position += Time.deltaTime * speed * transform.forward;
            }
        }

        //When cat is wandering and not going anywhere (current target reached/stuck)
        if ((wander && pathComplete()) || (wander && timerwaiting >= waitingtimer))
        {
            //Add to timer until it reaches the wandering interval
            if (timerwander >= wandertimer)
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
                    timerwander = 0;
                    timerwaiting = 0;
                    print(wandertarget);
                    Instantiate(targetpoint, wandertarget, Quaternion.identity);
                }

            }
        }

        //Once the time has come to target something to break
        if (wander && timertargeting >= targettimer)
        {
            //Set booleans to prevent wandering
            wander = false;
            targeting = true;

            //Array for targeting
            GameObject[] targets;

            //Populates array with all breakables in scene
            targets = GameObject.FindGameObjectsWithTag("Breakable");

            //Variables for upcoming for loop
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;

            //Goes through each breakable in scene
            //Keeps closest object so far, until all objects are iterated
            foreach (GameObject target in targets)
            {
                Vector3 diff = target.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = target;
                    distance = curDistance;
                }
            }

            //Assigns target object as "closest" from above
            currenttarget = closest;

            //Sets new pathfinding destination for assigned target's position
            agent.SetDestination(currenttarget.transform.position);
        }
    }
}
