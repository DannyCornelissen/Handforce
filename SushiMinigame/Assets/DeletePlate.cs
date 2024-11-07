using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlate : MonoBehaviour
{
    [SerializeField]
    float tolerance = 0.1f; // Tolerance to determine how close positions should be

    void Update()
    {
        // Find all instances of the Plate prefab by tag
        GameObject[] plates = GameObject.FindGameObjectsWithTag("Plate");

        foreach (GameObject plate in plates)
        {
            // Check if the clone's position matches the desired position within the tolerance range
            if (Vector3.Distance(transform.position, plate.transform.position) < tolerance)
            {
                Destroy(plate); // Only deletes the specific plate that meets the criteria
            }
        }
    }
}
