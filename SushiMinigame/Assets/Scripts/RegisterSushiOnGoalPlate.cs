using System.Collections;
using UnityEngine;

public class RegisterSushiOnGoalPlate : MonoBehaviour
{
    public static int score = 0; // Static score variable, shared across all sushi
    public SushiSpawn sushiSpawn; // Get the script for the Sushi respawn mechanics
    public bool isSushiOnGoalPlate = false;
    public bool hasCollided = false;

    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hasCollided)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Sushi"))
        {
            hasCollided = true;
            Debug.Log("Sushi landed on Plate!");
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
        GameObject[] sushi = GameObject.FindGameObjectsWithTag("Sushi");
        Destroy(sushi[0]);
        sushiSpawn.CreateNewPlate();
        hasCollided = false;

        StartCoroutine(sushiNoLongerOnGoalPlate(3f));
    }

    private IEnumerator sushiNoLongerOnGoalPlate(float delay)
    {
        yield return new WaitForSeconds(delay);
        isSushiOnGoalPlate = false;
    }

}
