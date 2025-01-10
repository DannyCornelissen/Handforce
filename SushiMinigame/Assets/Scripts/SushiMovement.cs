using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SushiMovement : MonoBehaviour
{
    //Array of all the waypoints.
    [SerializeField] private Transform[] waypoints;

    //Speed at wich the object should travel.
    [SerializeField] private float speed;

    private int nextWayPoint = 0;
    private int stopPoint;
    private bool _readyForDecouple = false;


    public bool ReadyForDecouple
    {
        get { return _readyForDecouple; }
    }


    // Start is called before the first frame update
    void Start()
    {
        int index = 0;



        foreach (Transform waypoint in waypoints)
        {
            if (waypoint.tag == "StopPoint")
            {
                stopPoint = index;
            }
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject sushi = GameObject.FindGameObjectWithTag("SushiCollisionChecker");


        if (transform.position != waypoints[stopPoint].position)
        {
            MoveToWaypoint(waypoints[nextWayPoint]);
        }
        else if (transform.position == waypoints[stopPoint].position)
        {
            if (sushi != null)
            {
                SushiCollisionChecker checker = sushi.GetComponent<SushiCollisionChecker>();
                _readyForDecouple = true;
                if (checker.CollidedBarCounter || checker.CollidedGoalPlate || checker.CollidedOriginalPlate)
                {
                    MoveToWaypoint(waypoints[nextWayPoint]);
                }

            }
        }

        if (transform.position == waypoints[nextWayPoint].position && nextWayPoint <= waypoints.Count())
        {
            nextWayPoint++;
        }
    }
    public void MoveToWaypoint(Transform Waypoint)
    {
        transform.position = Vector3.MoveTowards(transform.position, Waypoint.position, speed * Time.deltaTime);
    }
}
