using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // CARD COMPONENTS
    [Header("Card Components")]

    [Tooltip("The card's outline when selected")]
    public GameObject activeMarker;

    // The game's Game Controller
    GameController gameController;

    // CARD ATTRIBUTES
    [Header("Card Attributes")]

    [Tooltip("The color of the player currently controlling this card")]
    public string cardColor;

    [Tooltip("The positions this card allows units to move to")]
    public Vector2[] movementPoints;

    [Tooltip("Returns true if card is selected")]
    public bool isActiveCard = false;


    // Start is called before the first frame update
    void Start()
    {
        // Defines the GameController object
        gameController = FindObjectOfType<GameController>();
    }

    // Makes this card the active card or deselects it if already active
    public void MakeActiveCard()
    {
        // Checks if the card belongs to the current player taking his/her turn
        if (cardColor != gameController.currentTurnColor)
        {
            // Ends the function if check is false
            return;
        }

        // Checks if the card is currently selected
        if (isActiveCard)
        {
            // Clears the active card
            gameController.ClearActiveCard();
            // Ends the function
            return;
        }

        // Clears any active card
        gameController.ClearActiveCard();

        // Sets this card as the active card
        gameController.activeCard = this;
        // Declares that this card is active
        isActiveCard = true;
        //Turns on the Active Marker
        activeMarker.SetActive(true);
        // Shows available moves based on active card and unit
        gameController.ShowAvailableMoves();
    }
}
