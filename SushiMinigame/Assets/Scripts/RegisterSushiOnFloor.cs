using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RegisterSushiOnFloor : MonoBehaviour
{
    //private Rigidbody rb;
    //private Vector3 startPosition; // Stores the initial position of the sushi
    //private Quaternion startRotation; // Stores the initial rotation of the sushi
    public bool isSushiOnFloor = false;
    public SushiSpawn sushiSpawn; // Get the script for the Sushi respawn mechanics


    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //startPosition = transform.position; // Save the starting position
        //startRotation = transform.rotation; // Save the starting rotation
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sushi"))
        {
            Debug.Log("Sushi landed on Floor!");
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            //rb.isKinematic = true;

            isSushiOnFloor = true;

            // Start the respawn process
            StartCoroutine(RespawnAfterDelay(2f));
        }
    }
    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset the sushi's position and rotation
        //transform.position = startPosition;
        //transform.rotation = startRotation;
        //rb.isKinematic = false;

        // Re-enable physics
        sushiSpawn.CreateNewPlate();
        isSushiOnFloor = false;

    }
}
