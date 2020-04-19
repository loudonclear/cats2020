using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVScreen : MonoBehaviour
{
    //The dollar value of the room
    public int value;

    //The text element of the tv
    private Text message;

    //The message to be put up on the tv
    private string tempmessage;

    // Start is called before the first frame update
    void Start()
    {
        //Set the starting value for the room
        value = 5000;

        //Get the text component
        message = GetComponent<Text>();

        //Generate and post the starting message (can be changed later if necessary)
        tempmessage = "Room value: $" + value.ToString();
        message.text = tempmessage;
    }

    // Update is called once per frame
    void Update()
    {
        //Update the tempmessage and post it to the tv text component
        tempmessage = "Room value: $" + value.ToString();
        message.text = tempmessage;
    }
}
