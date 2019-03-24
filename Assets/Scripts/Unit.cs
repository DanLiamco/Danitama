using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // UNIT COMPONENTS
    [Header("Unit Components")]

    [Tooltip("The unit's outline when selected")]
    public GameObject activeMarker;

    // The game's Game Controller
    GameController gameController;

    // UNIT ATTRIBUTES
    [Header("Unit Attributes")]

    // Returns true if this unit is the King
    public bool isKing = false;

    // The unit's color
    public string unitColor;

    // Returns true if this Unit is selected
    public bool isActiveUnit = false;

    // Start is called before the first frame update
    void Start()
    {
        // Defines the Game Controller
        gameController = FindObjectOfType<GameController>();
    }

    // Checks if a trigger collides with the unit's collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if Unit is the same color as the player taking his/her turn
        if (unitColor == gameController.currentTurnColor)
        {
            // Returns highlight to its pool
            collision.transform.localPosition = new Vector2(0f, 0f);
        } /* This means unit is not same color as player taking his/her turn */ else
        {
            // Defines unit as an eatable unit
            collision.GetComponent<Highlight>().canEatUnit = this;
        }
    }

    // Makes this unit the active unit or deselects it if already active
    public void MakeActiveUnit()
    {
        // Checks if Unit is the same color as the player taking his/her turn
        if (unitColor != gameController.currentTurnColor)
        {
            // Ends the function if check is false
            return;
        }

        // Check if unit is the active unit
        if (isActiveUnit)
        {
            // Clears the current active unit
            gameController.ClearActiveUnit();
            // Ends the function
            return;
        }

        // Clears the current active unit
        gameController.ClearActiveUnit();

        // Declares this unit as the active unit
        gameController.activeUnit = this;
        // Declares that this unit is active
        isActiveUnit = true;
        // Turns on Active Marker
        activeMarker.SetActive(true);
        // Shows available moves based on active unit and card
        gameController.ShowAvailableMoves();
    }

    // Checks if Unit is a Blue King on a Red Throne and triggers win condition if true
    public void CheckKingBlue()
    {
        // Defines the location of the Red Throne
        Vector3 redThrone = new Vector3(2.5f, 4.5f);

        // Checks if Unit is a Blue King on a Red Throne
        if (isKing && unitColor == "Blue" && transform.position == redThrone)
        {
            // Triggers Win Condition
            FindObjectOfType<GameController>().Win(unitColor);
        }
    }

    // Checks if Unit is a Red King on a Blue Throne and triggers win condition if true
    public void CheckKingRed()
    {
        // Initializes the location of the Blue Throne
        Vector3 blueThrone = new Vector3(2.5f, 0.5f);

        // Checks if Unit is a Red King on a Blue Throne
        if (isKing && unitColor == "Red" && transform.position == blueThrone)
        {
            // Triggers Win Condition
            FindObjectOfType<GameController>().Win(unitColor);
        }
    }
}
