using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    //HIGHLIGHT ATTRIBUTES
    [Header("Highlight Attributes")]

    [Tooltip("The unit to be eaten if player moves his unit to this area")]
    public Unit canEatUnit;

    // Eats the unit in the selected space
    public void EatUnit()
    {
        // Checks if the highlight is on an eatable unit
        if (canEatUnit)
        {
            // Repositions eatable unit off the board
            canEatUnit.transform.position = new Vector2(20f, 20f);
            // Checks Win Condition
            FindObjectOfType<TeamsManager>().UnitEaten(canEatUnit);
        }
    }
}
