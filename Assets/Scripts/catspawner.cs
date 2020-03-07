using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catspawner : MonoBehaviour
{
    //The cat prefab
    public GameObject cat;
    
    //How long in seconds to wait for the next cat to spawn
    public float spawntimer;
    
    //Target object for cat after spawning
    public GameObject entertarget;
    
    // Start is called before the first frame update
    void Start()
    {
        //Assign cat spawn timer
        spawntimer = 5;

        //Start initial timer to wait for first spawn
        StartCoroutine(CatSpawnTimer());
    }

    IEnumerator CatSpawnTimer()
    {
        //Wait for next spawn
        yield return new WaitForSeconds(spawntimer);

        //Create next cat at spawner
        Instantiate(cat, transform.position, transform.rotation);

        //Restart timer to repeat ad infinitum
        StartCoroutine(CatSpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
