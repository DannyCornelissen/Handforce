using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiCollisionChecker : MonoBehaviour
{
    public SushiSpawn sushiSpawn;
    private bool collidedBarCounter;
    private bool collidedGoalPlate;
    private bool collidedOriginalPlate;
    private int collisionCount = 0;
    private ScoreUI scoreUI;

    public bool CollidedBarCounter { get { return collidedBarCounter; }  }
    public bool CollidedGoalPlate { get { return collidedGoalPlate; } }
    public bool CollidedOriginalPlate { get { return collidedOriginalPlate; } }
    // Start is called before the first frame update
    void Start()
    {
        scoreUI = GameObject.Find("Score").GetComponent<ScoreUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other )
   {
        switch (other.gameObject.tag)
        {
            case ("GoalPlate"):
                registerGoalPlate();
                break;

            case ("BarCounter"):
                collidedBarCounter = true;
                collisionCount++;
                if (collidedGoalPlate || collidedOriginalPlate) return;
                StartCoroutine(RespawnAfterDelay(2f));
                break;

            default:
                break;
            case ("Plate"):
                if(gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().useGravity == true)
                {
                    collidedOriginalPlate = true;
                    collisionCount++;
                    StartCoroutine(RespawnAfterDelay(2f));
                }

                break;
        }

    }

    private void registerGoalPlate()
    {
        Debug.Log(collidedBarCounter);
        collidedGoalPlate = true;
        collisionCount++;
        Debug.Log(collisionCount);
        if (collidedBarCounter) return;
        StartCoroutine(RespawnAfterDelay(2f));
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UpdateScore();
        sushiSpawn.CreateNewPlate();
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void UpdateScore()
    {
        if (collisionCount == 1)
        {
            if (collidedGoalPlate) scoreUI.UpdateScore();
        }
    }
}
