
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControlScript : MonoBehaviour
{
    public Transform indexFingerBone;  
    public Transform middleFingerBone;
    public Transform ThumbBone;
    public Transform palm;

    public float rotationSpeed = 50f;   

    void Update()
    {
        // Control the index finger with WASD keys
        if (Input.GetKey(KeyCode.W))
        {
            indexFingerBone.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            indexFingerBone.Rotate(Vector3.left, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            indexFingerBone.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            indexFingerBone.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // Control the middle finger with Arrow keys
        if (Input.GetKey(KeyCode.UpArrow))
        {
            middleFingerBone.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            middleFingerBone.Rotate(Vector3.left, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            middleFingerBone.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            middleFingerBone.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // Control the thumb with Arrow keys
        if (Input.GetKey(KeyCode.Keypad8))
        {
            ThumbBone.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            ThumbBone.Rotate(Vector3.left, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            ThumbBone.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            ThumbBone.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // Control the palm with R, T, U and Y
        if (Input.GetKey(KeyCode.R))
        {
            palm.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.T))
        {
            palm.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Y))
        {
            palm.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.U))
        {
            palm.Rotate(Vector3.left, -rotationSpeed * Time.deltaTime);
        }
    }
}
