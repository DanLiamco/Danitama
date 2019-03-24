using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsManager : MonoBehaviour
{
    // TEAMS MANAGER ATTRIBUTES
    [Header("Teams Manager Attributes")]

    [Tooltip("The Red Team units")]
    public Unit[] redTeam;
    [Tooltip("The Blue Team units")]
    public Unit[] blueTeam;

    // Checks win condition
    public void UnitEaten(Unit eatenUnit)
    {
        // Checks if eaten unit is a King
        if (eatenUnit.isKing)
        {
            // Initializes win Color
            string winColor;

            // Check if eaten unit is Blue
            if (eatenUnit.unitColor == "Blue")
            {
                // Sets win color to Red
                winColor = "Red";
            } /* Checks if eaten unit is not Blue (usually means it is Red) */else
            {
                // Sets win color to Blue
                winColor = "Blue";
            }

            // Trigger Win
            FindObjectOfType<GameController>().Win(winColor);
        }
    }
}
