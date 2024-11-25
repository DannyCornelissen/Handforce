using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class SushiPickup : MonoBehaviour
{
    string fingerSide;
    string thumbSide;


    private GameObject thumb;
    private Transform sushi;
    private bool isParentSet = false;
    public float speed = 5f;      
    public float maxDistance = 10f;    
    private Vector3 startPosition;
    private bool backwardInputReceived = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if( fingerSide != null && thumbSide != null && fingerSide != thumbSide)
        {
            sushi.SetParent(thumb.transform);
            backwardInputReceived = true;
}
        else if(fingerSide == null && sushi != null)
        {
            sushi.parent = null;
        }
        
        float distanceMoved = Vector3.Distance(startPosition, transform.position);
        if (distanceMoved < maxDistance && backwardInputReceived)
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.World);
        }
    }



    public void OnCollisionDetected(string side, GameObject finger, Transform Sushi )
    {
        sushi = Sushi;
        if (finger.tag == "Thumb")
        {
            thumbSide = side;
            thumb = finger;
        }
  
        if(finger.tag == "IndexFinger")
        {

            fingerSide = side;
        }
    }

    public void OnCollisionEnded(string side, GameObject finger)
    {
        if (finger.tag == "Thumb")
        {
            thumbSide = null; // Clear thumbSide when the thumb exits
        }

        if (finger.tag == "IndexFinger")
        {
            fingerSide = null;
        }
    }
}
