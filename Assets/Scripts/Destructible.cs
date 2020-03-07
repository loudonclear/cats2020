using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = transform.position;
        GameObject pieces = Instantiate(destroyed, transform.position, transform.rotation);
        pieces.GetComponent<Rigidbody>().AddExplosionForce(5.0f, explosionPos, 3.0f, 3.0F);
        Destroy(gameObject);
    }
}
