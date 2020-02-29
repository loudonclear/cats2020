using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catmovement : MonoBehaviour
{

    //Speed value; higher = faster cat
    public float speed = 0.15f;

    //Turn interval value; how many seconds to move during movement phase before deciding on a turn movement
    public float turninterval = 2f;

    //How many times to walk around before trying to acquire a target
    public int targetinterval;

    //Timer variable to count down until cat targets something
    public int targettimer = 0;

    //Targeting boolean to check if cat is going after a breakable
    public bool targeting = false;

    //Walking boolean to check if cat is just walking forward
    public bool walking = true;

    //"Heading"; a rotation value in degrees from 0 to 360 (rotating around y-axis)
    public float heading = 0f;

    //Game object that will be target of cat
    GameObject currenttarget;

    //Declare rigidbody
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //Assign rigidbody
        rigidbody = GetComponent<Rigidbody>();

        //Set heading to a random value for now; will probably restrict this when cats spawn in
        //heading = Random.Range(0, 360);
        heading = 0;

        //Set a random targetting interval, so cats don't always acquire targets at the same time
        targetinterval = 2;

        //Rotate cat to face movement direction
        transform.Rotate(0, heading, 0);

        //Set walking to true to give cat time to move before first turn
        walking = true;

        //Initiate the turn timer to begin the cat's random wandering routine
        StartCoroutine(TurnTimer());
    }

    //Timer for how long to walk before stopping to turn
    //Also iterates the timer for the targeting function
    //Calls Target() when timer hits target, otherwise calls Turn()
    IEnumerator TurnTimer()
    {
        //Wait a few seconds before starting the next turn
        yield return new WaitForSeconds(1);

        //Set walking to false to stop cat for turn
        walking = false;
        
        //Increase target timer
        targettimer += 1;

        //Check targeting timer
        if (targettimer == targetinterval)
        {
            //Initiate targeting systems
            Target();
        }

        else
        {
            //Make the cat turn by starting void Turn
            StartCoroutine(Turn());
        }

    }

    //Turns the cat and restarts the turn timer
    IEnumerator Turn()
    {
        //Make the cat stop for a second when turning
        yield return new WaitForSeconds(0.5f);
        
        //Assign a random new heading value
        heading = Random.Range(0, 360);

        //Rotate cat to face movement direction
        transform.Rotate(0, heading, 0);

        //Turn the cat's walking back on
        walking = true;

        //Restart the timer to keep the wandering going
        StartCoroutine(TurnTimer());
    }
    
    //Chooses a target for the cat
    //Currently the closest target
    void Target()
    {
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

        //Assigns target as "closest" from above
        currenttarget = closest;

        //Make cat face the target
        transform.LookAt(currenttarget.transform);

        //Sets the cat to walking
        walking = true;

        //Sets the cat to targeting
        targeting = true;
    }

    //Calls whenever a cat collides with a collider
    void OnTriggerEnter(Collider other)
    {
        //Find object responsible for collision
        GameObject collision = other.gameObject;

        //Check if object is the target, and if cat is the one targeting the object
        if (collision == currenttarget && targeting == true)
        {
            //Destroy the target
            Destroy(currenttarget);

            //Reset the targeting timer
            targettimer = 0;

            //Restart the cat moving around by calling TurnTimer()
            StartCoroutine(TurnTimer());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (walking == true)
        {
            transform.position += Time.deltaTime * speed * transform.forward;
        }
    }
}
