using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakage : MonoBehaviour
{

    public int itemvalue;

    GameObject tv;

    GameObject tvscreen;

    TVmessage TVmessage;

    // Start is called before the first frame update
    void Start()
    {
        tv = GameObject.Find("tv");
        tvscreen = GameObject.Find("Description");
        TVmessage = tvscreen.GetComponent<TVScreen>();
        itemvalue = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        TVmessage.value -= itemvalue;
    }
}
