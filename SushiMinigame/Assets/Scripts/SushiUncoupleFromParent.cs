using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiUncoupleFromParent : MonoBehaviour
{
    SushiMovement sushiMovement;
    private bool sushiDecoupledFromPlate = false;

    // Start is called before the first frame update
    void Start()
    {
      sushiMovement = transform.parent.GetComponent<SushiMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sushiMovement.ReadyForDecouple && !sushiDecoupledFromPlate)
        {
            transform.parent = null;
            sushiDecoupledFromPlate = true;
        }
    }
}
