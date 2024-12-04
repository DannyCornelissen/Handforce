using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RegisterSushiOnFloor : MonoBehaviour
{

    public bool isSushiOnFloor = false;
    public SushiSpawn sushiSpawn; // Get the script for the Sushi respawn mechanics
    public RegisterSushiOnGoalPlate registerSushiOnGoalPlate;


    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
       if(isSushiOnFloor)
        {
            return;
        }
        
        if (collision.gameObject.CompareTag("Sushi"))
        {
            Debug.Log("Sushi landed on Floor!");

            isSushiOnFloor = true;

            // Start the respawn process
            StartCoroutine(RespawnAfterDelay(2f));
        }
    }
    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject[] sushi = GameObject.FindGameObjectsWithTag("Sushi");
        if(registerSushiOnGoalPlate.isSushiOnGoalPlate)
        {
            RegisterSushiOnGoalPlate.score -= 1;
        }
        else
        {
            Destroy(sushi[0]);
            sushiSpawn.CreateNewPlate();
        }
   
        StartCoroutine(sushiNoLongerOnFloor(3f));
    }

    private IEnumerator sushiNoLongerOnFloor(float delay)
    {
        yield return new WaitForSeconds(delay);
        isSushiOnFloor = false;
    }
}
