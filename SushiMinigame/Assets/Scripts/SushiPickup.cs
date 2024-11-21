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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( fingerSide != null && thumbSide != null && fingerSide != thumbSide)
        {
            sushi.SetParent(thumb.transform);
        }
    }
    public void OnCollisionDetected(string side, GameObject finger, Transform Sushi )
    {
        sushi = Sushi;
        if(finger.tag == "Thumb")
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
            fingerSide = null; // Clear fingerSide when the index finger exits
        }
    }
}
