using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVScreen : MonoBehaviour
{
    public int value;

    private Text message;

    private string tempmessage;

    // Start is called before the first frame update
    void Start()
    {
        value = 5000;
        message = GetComponent<Text>();
        tempmessage = "Room value: $" + value.ToString();
        message.text = tempmessage;
    }

    // Update is called once per frame
    void Update()
    {
        tempmessage = "Room value: $" + value.ToString();
        message.text = tempmessage;
    }
}
