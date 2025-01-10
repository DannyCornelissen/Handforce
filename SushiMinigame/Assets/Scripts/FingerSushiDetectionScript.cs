using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FingerSushiDetectionScript : MonoBehaviour
{
    public SushiPickup sushiPickup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        // Check if the collided object is the sushi's target collider
        if (other.name == "collisionLeft")
        {
            sushiPickup.OnCollisionDetected("collisionLeft", gameObject, other.transform.parent);
        }
        if(other.name == "collisionRight")
        {
            sushiPickup.OnCollisionDetected("collisionRight", gameObject, other.transform.parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        sushiPickup?.OnCollisionEnded(other.gameObject.name, gameObject);
    }

}
