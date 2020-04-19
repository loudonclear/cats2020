using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breakage : MonoBehaviour
{
    //The item's specific value in dollars
    public int itemvalue;

    //The tv object from the scene
    GameObject tv;

    //The text object from the tv from the scene
    GameObject tvscreen;

    // Start is called before the first frame update
    void Start()
    {
        //Get the tv object
        tv = GameObject.Find("tv");

        //Get the tv text object
        tvscreen = GameObject.Find("Description");

        //Set the item's value (might want to delete and modify in the editor once
        //objects with different values are added)
        itemvalue = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the object is destroyed by the cats (may replace trigger depending on
    // breakage mechanics later)
    void OnDestroy()
    {
        //Subtract the item's dollar value from the value on the tv text object
        //This updates the tv with a new, lower room value
        tvscreen.GetComponent<TVScreen>().value -= itemvalue;
    }
}
