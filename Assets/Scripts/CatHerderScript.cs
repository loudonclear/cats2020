using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHerderScript : MonoBehaviour
{
    //Time in seconds for next object to be targeted
    float untilnexttarget;

    //Timer until next targeting event
    float targetingtimer;

    //Minimum time for next targeting
    float timermin;

    //Maximum time for next targeting
    float timermax;
    
    // Start is called before the first frame update
    void Start()
    {
        //Set the first time in seconds until the first targeting begins
        untilnexttarget = 5;

        //Set the timer to 0 to start
        targetingtimer = 0;

        //Set initial min and max timer values
        timermin = 10;
        timermax = 30;
    }

    // Update is called once per frame
    void Update()
    {
        //Update timer
        targetingtimer += Time.deltaTime;

        //Once the time has reached the prescribed time in seconds to target
        if (targetingtimer >= untilnexttarget)
        {
            //Create an array of all cats in scene (all objects with a "catnavigation" script)
            catnavigation[] catarray = FindObjectsOfType<catnavigation>();

            //Randomly choose one cat in array
            catnavigation chosencat = catarray[Random.Range(0, catarray.Length)];

            //Make sure the chosen cat is not already busy
            if (chosencat.targeting == false)
            {
                //Change the cat's "lottery" boolean to true, initiation targeting methods in said cat's "catnavigation" script)
                chosencat.lottery = true;

                //Reset the time until the next targeting happens (in seconds)
                untilnexttarget = Random.Range(timermin, timermax);

                //Reset the timer for said targeting
                targetingtimer = 0;

                //debug print
                print("A cat has been chosen to target a new... target");
            }
        }
    }
}
