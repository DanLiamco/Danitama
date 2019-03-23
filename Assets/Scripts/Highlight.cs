using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Unit canEatUnit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatUnit()
    {
        if (canEatUnit)
        {
            canEatUnit.transform.position = new Vector2(20f, 20f);

            if (canEatUnit.unitColor == "Blue")
            {
                FindObjectOfType<TeamsManager>().BlueUnitEaten(canEatUnit);
            } else
            {
                FindObjectOfType<TeamsManager>().RedUnitEaten(canEatUnit);
            }
        }
    }
}
