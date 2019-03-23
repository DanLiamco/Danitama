using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsManager : MonoBehaviour
{
    public Unit[] redTeam;
    public Unit[] blueTeam;

    public int redUnitsEaten = 0;
    public int blueUnitsEaten = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlueUnitEaten(Unit eatenUnit)
    {
        blueUnitsEaten++;
        if (blueUnitsEaten >= 5)
        {
            FindObjectOfType<GameController>().Win("Red");
        }

        if (eatenUnit.isKing)
        {
            FindObjectOfType<GameController>().Win("Red");
        }
    }

    public void RedUnitEaten(Unit eatenUnit)
    {
        redUnitsEaten++;
        if (redUnitsEaten >= 5)
        {
            FindObjectOfType<GameController>().Win("Blue");
        }

        if (eatenUnit.isKing)
        {
            FindObjectOfType<GameController>().Win("Blue");
        }
    }
}
