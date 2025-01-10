using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopstickMovement : MonoBehaviour
{
    public float speed = 5f;           // Speed of movement
    public float maxDistance = 10f;    // Maximum distance allowed
    private Vector3 startPosition;      // Starting position of the chopsticks
    private bool forwardInputReceived = false;
    private bool backwardInputReceived = false;


    void Start()
    {
        // Save the starting position when the game begins
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            forwardInputReceived = true;
        }


        float distanceMoved = Vector3.Distance(startPosition, transform.position);

        if (forwardInputReceived)
        {
            // Move forward if the distance is within the limit
            if (distanceMoved < maxDistance)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                forwardInputReceived = false;
            }
        }

    }
}
