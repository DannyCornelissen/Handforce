using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (string controller in Input.GetJoystickNames())
        {
            if (controller == "")
            {
                Debug.Log("No controller connected");
            }
            Debug.Log(controller);
        }

    }
}
