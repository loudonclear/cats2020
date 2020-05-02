using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StartGame : MonoBehaviour
{
    public GameObject[] menu;

    private 
    void Start()
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectTouched += new InteractableObjectEventHandler(ObjectGrabbed);
        //GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
    }

    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        foreach (GameObject obj in menu)
        {
            obj.SetActive(false);
        }
    }
}
