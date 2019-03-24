using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    // CARD ATTRIBUTES
    [Header("Card Attributes")]

    [Tooltip("The next card in queue to be made available to the Red Player")]
    public Card nextRedCard;
    [Tooltip("The next card in queue to be made available to the Blue Player")]
    public Card nextBlueCard;

    // Sets the next card for players
    public void SetNextCard(Card activeCard)
    {
        // Checks if the card used is Blue
        if(activeCard.cardColor == "Blue")
        {
            // Checks if a card is in queue to be made available to the Blue player
            if (nextBlueCard)
            {
                // Moves queued Blue card to the position of the Blue card used
                nextBlueCard.transform.position = activeCard.transform.position;
                // Changes card "color" to Blue
                nextBlueCard.cardColor = "Blue";
            }

            // Queues this as the Red Player's next card
            nextRedCard = activeCard;
            // Moves the active card into the Red queued card position
            activeCard.transform.position = new Vector2(2.5f, 7f);

            // Rotates the card 180 degrees if it is in the Red queued card position
            Quaternion newRotation = Quaternion.Euler(0f, 0f, 180f);
            nextRedCard.transform.rotation = newRotation;

        } /* Checks if the card used is not Blue (usually this means it's Red)*/ else
        {
            // Checks if a card is in queue to be made available to the Red player
            if (nextRedCard)
            {
                // Moves queued Red card to the position of the Red card used
                nextRedCard.transform.position = activeCard.transform.position;
                // Changes card "color" to Red
                nextRedCard.cardColor = "Red";
            }

            // Queues this as the Blue Player's next card
            nextBlueCard = activeCard;
            // Moves the active card into the Blue queued card position
            activeCard.transform.position = new Vector2(2.5f, -2f);

            // Rotates the card 0 degrees if it is in the Blue queued card position
            Quaternion newRotation = Quaternion.Euler(0f, 0f, 0f);
            nextBlueCard.transform.rotation = newRotation;
        }
    }
}
