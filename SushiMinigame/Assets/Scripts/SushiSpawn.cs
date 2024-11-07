using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject Spawner;


    [SerializeField] List<GameObject> Sushi; 

    GameObject CreatedObject;
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this checks if CreatedObject is null
        if (CreatedObject == null)
        {
            //Generates a random number
            int rng = Random.Range(0, Sushi.Count);

            //Creates object taken from prefabs in a list randomly using the generated number.
            //It then adds it to CreatedObject making it not null wich means no items can be added anymore. 
            //After that it sets the object to spawner wich is an object in the scene that also holds this script using its location it sets the location of the sushi
            CreatedObject = Instantiate(Sushi[rng], Spawner.transform.position, Sushi[rng].transform.rotation);
        }
    }
}
