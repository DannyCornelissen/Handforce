using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiPickup : MonoBehaviour
{
    private GameObject firstFinger;
    private GameObject secondFinger;
    private Transform sushi;
    public float speed = 5f;
    public float maxDistance = 10f;
    public float minDistance;
    private Vector3 startPosition;
    private bool backwardInputReceived = false;
    private bool sushiAttached = false;
    private RegisterSushiOnGoalPlate goalPlate;
    private RegisterSushiOnFloor floor;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        goalPlate = GameObject.Find("GoalPlate").GetComponent<RegisterSushiOnGoalPlate>();
        floor = GameObject.Find("Floor").GetComponent<RegisterSushiOnFloor>();
        if (firstFinger != null && secondFinger != null && firstFinger != secondFinger)
        {
          sushi.GetComponent<Rigidbody>().isKinematic = true;
          backwardInputReceived = true;
          sushi.SetParent(firstFinger.transform);
          sushiAttached = true;
        }

        else if (secondFinger == null && sushi != null && sushiAttached == true)
        {
            sushi.SetParent(null);
            sushi.GetComponent<Rigidbody>().isKinematic = false;
            sushi.GetComponent<Rigidbody>().useGravity = true;
            sushiAttached = false;
        }

        float distanceMoved = Vector3.Distance(startPosition, transform.position);
        if (distanceMoved < maxDistance && backwardInputReceived)
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.World);
        }
      
        if(!sushiAttached && distanceMoved > minDistance)
        {
            backwardInputReceived = false;
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }

    }

    public void OnCollisionDetected(string side, GameObject finger, Transform Sushi)
    {
        sushi = Sushi;

        if (side == "collisionRight")
        {
            firstFinger = finger;
        }

        if (side == "collisionLeft")
        {
            secondFinger = finger;
        }
    }

    public void OnCollisionEnded(string side, GameObject finger)
    {
        if (finger == firstFinger)
        {
            firstFinger = null;
        }

        if (finger == secondFinger)
        {
            secondFinger = null;
        }
    }
}
