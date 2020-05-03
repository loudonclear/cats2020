using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StartGame : MonoBehaviour
{
    public GameObject[] menu;
    public GameObject spawners;

    private 
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic("Menu");
        GetComponent<VRTK_InteractableObject>().InteractableObjectTouched += new InteractableObjectEventHandler(ObjectGrabbed);
    }

    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        foreach (GameObject obj in menu)
        {
            obj.SetActive(false);
        }
        spawners.SetActive(true);

        FindObjectOfType<AudioManager>().PlayMusic("Level");
    }
}
