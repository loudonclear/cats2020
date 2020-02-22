using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catmovement : MonoBehaviour
{

    //Speed value; higher = faster cat
    public float speed = 0.15f;

    //Turn interval value; how many seconds to move during movement phase before deciding on a turn movement
    public float turninterval = 2f;

    //Targetting boolean to check if cat is going after a breakable
    public bool targetting = false;

    //Walking boolean to check if cat is just walking forward
    public bool walking = true;

    //"Heading"; a rotation value in degrees from 0 to 360 (rotating around y-axis)
    public float heading = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Set heading to a random value for now; will probably restrict this when cats spawn in
        heading = Random.Range(0, 360);

        //Rotate cat to face movement direction
        transform.Rotate(0, heading - 45, 0);

        //Set walking to true to give cat time to move before first turn
        walking = true;

        //Initiate the turn timer to begin the cat's random wandering routine
        StartCoroutine(TurnTimer());
    }

    IEnumerator TurnTimer()
    {
        //Wait a few seconds before starting the next turn
        yield return new WaitForSeconds(1);

        //Set walking to false to stop cat for turn
        walking = false;

        //Make the cat turn by starting void Turn
        StartCoroutine(Turn());
    }

    IEnumerator Turn()
    {
        //Make the cat stop for a second when turning
        yield return new WaitForSeconds(1);
        
        //Assign a random new heading value
        heading = Random.Range(0, 360);

        //Rotate cat to face movement direction
        transform.Rotate(0, heading - 45, 0);

        //Turn the cat's walking back on
        walking = true;

        //Restart the timer to keep the wandering going
        StartCoroutine(TurnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (walking == true)
        {
            transform.position += Time.deltaTime * speed * new Vector3(Mathf.Sin(heading), 0, Mathf.Cos(heading));
        }
    }
}
