using System.Collections;
using UnityEngine;

public class RegisterSushiOnGoalPlate : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPosition; // Stores the initial position of the sushi
    private Quaternion startRotation; // Stores the initial rotation of the sushi
    public static int score = 0; // Static score variable, shared across all sushi
    public SushiSpawn sushiSpawn; // Get the script for the Sushi respawn mechanics
    public bool isSushiOnGoalPlate = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position; // Save the starting position
        startRotation = transform.rotation; // Save the starting rotation
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plate"))
        {
            Debug.Log("Sushi landed on Plate!");
            // Stop the sushi's movement
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            isSushiOnGoalPlate = true;

            // Increase the score
            score += 1;
            Debug.Log("Score: " + score);

            // Start the respawn process
            StartCoroutine(RespawnAfterDelay(2f));
        }
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset the sushi's position and rotation
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Re-enable physics
        rb.isKinematic = false;
        sushiSpawn.CreateNewPlate();
        isSushiOnGoalPlate = false;

    }
}
