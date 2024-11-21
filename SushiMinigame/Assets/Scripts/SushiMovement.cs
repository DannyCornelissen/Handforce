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
    private RegisterSushiOnGoalPlate goalPlate;
    private RegisterSushiOnFloor floor;

    public bool ReadyForDecouple
    {
        get { return _readyForDecouple; }
    }


    // Start is called before the first frame update
    void Start()
    {
       int index = 0;

       goalPlate = GameObject.Find("SushiOne").GetComponent<RegisterSushiOnGoalPlate>();
       floor = GameObject.Find("Floor").GetComponent<RegisterSushiOnFloor>();

        foreach (Transform waypoint in waypoints)
       {
         if(waypoint.tag ==  "StopPoint")
         {
            stopPoint = index;
         }
         index++;
       }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != waypoints[stopPoint].position)
        {
            MoveToWaypoint(waypoints[nextWayPoint]);
        }
        else if(transform.position == waypoints[stopPoint].position)
        {
            _readyForDecouple = true;
            if (goalPlate.isSushiOnGoalPlate || floor.isSushiOnFloor)
            {
                MoveToWaypoint(waypoints[nextWayPoint]);
            }
        }


        if (transform.position == waypoints[nextWayPoint].position && nextWayPoint <= waypoints.Count())
        {
            nextWayPoint++;
        }
    }

    private void MoveToWaypoint(Transform Waypoint)
    {
      transform.position = Vector3.MoveTowards(transform.position, Waypoint.position, speed * Time.deltaTime);
    }
}
