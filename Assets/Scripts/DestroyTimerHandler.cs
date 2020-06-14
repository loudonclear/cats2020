using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyTimerHandler : MonoBehaviour
{
    //Camera object (should be VR camera in game scene)
    Camera camera;

    //Float to track time
    float timer;

    //Timer image
    public Image Image;

    // Start is called before the first frame update
    void Start()
    {
        //Assign camera (again, should be VR camera in game scene)
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Make timer look at camera so player can see it from anywhere
        transform.LookAt(camera.transform);
        transform.rotation = Quaternion.LookRotation(camera.transform.forward);
        transform.position = transform.parent.position;

        //Track time since timer started
        timer += Time.deltaTime;

        //Set timer image to correct fill amount (how much time out of current timer value has elapsed)
        Image.fillAmount = timer / 5;

        if (timer >= 5)
        {
            StartCoroutine(TimerOver());
        }
    }

    //When timer has ended
    IEnumerator TimerOver()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
