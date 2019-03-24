using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // GAME CONTROLLER COMPONENTS
    [Header("Game Controller Components")]

    [Tooltip("The Highlight Markers for available moves")]
    [SerializeField]
    GameObject[] highlights;

    [Tooltip("The text that says it's the Blue Player's turn")]
    [SerializeField]
    GameObject blueTurn;

    [Tooltip("The text that says it's the Red Player's turn")]
    [SerializeField]
    GameObject redTurn;

    [Tooltip("The win board where win text is located")]
    [SerializeField]
    GameObject winBoard;

    [Tooltip("The text that announces the winner")]
    [SerializeField]
    Text winText;

    // GAME CONTROLLER ATTRIBUTES
    [Header("Game Controller Attributes")]

    [Tooltip("The active card")]
    public Card activeCard;
    [Tooltip("The active unit")]
    public Unit activeUnit;

    [Tooltip("The color of the player taking his/her turn")]
    public string currentTurnColor;

    // Returns true if a unit is moving
    bool moving = false;

    // Returns true if a player has won
    bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        // Checks if a player has won
        if (gameOver)
        {
            // Checks if a player has left clicked
            if (Input.GetMouseButton(0))
            {
                // Reloads current scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    // Shows available moves based on active unit and card
    public void ShowAvailableMoves()
    {
        // Initializes and defines flip sign
        float flipSign = 1f;

        // Checks if there is an active unit and card
        if (!activeCard || !activeUnit)
        {
            // Returns highlights to pool and ends function if there is no active unit or no active card
            ResetHighlights();
            return;
        }

        // Checks if active unit is Blue
        if (activeUnit.unitColor == "Blue")
        {
            // Sets flipsign to 1
            flipSign = 1f;
        } /* Checks if the card used is not Blue (usually this means it's Red)*/ else
        {
            // Sets flipsign to -1
            flipSign = -1f;
        }

        // Goes through the active cards list of movement points
        for (int i = 0; i < activeCard.movementPoints.Length; i++)
        {
            // Initializes and defines targetX as the current movement point's X position, flips it if it's a Red card
            float targetX = activeUnit.transform.position.x + activeCard.movementPoints[i].x * flipSign;
            // Initializes and defines targetY as the current movement point's Y, flips it if it's a Red card
            float targetY = activeUnit.transform.position.y + activeCard.movementPoints[i].y * flipSign;

            // If target X is beyond the map, go to the next iteration
            if (targetX < 0f || targetX > 5f)
            {
                continue;
            }
            // If target Y is beyond the map, go to the next iteration
            if (targetY < 0f || targetY > 5f)
            {
                continue;
            }

            // Sets a highlight marker's position to the target position
            highlights[i].transform.position = new Vector2(targetX, targetY);
        }
    }

    // Clears the active card
    public void ClearActiveCard()
    {
        // Checks if there is no active card
        if (!activeCard)
        {
            // Ends the function
            return;
        }

        // Turns active card's active bool off
        activeCard.isActiveCard = false;
        // Turns off the active card's active marker
        activeCard.activeMarker.SetActive(false);
        // Removes the current active card
        activeCard = null;
        // Returns highlight markers to their pool
        ResetHighlights();
    }

    // Clears the active unit
    public void ClearActiveUnit()
    {
        // Checks if there is no active unit
        if (!activeUnit)
        {
            // Ends the function
            return;
        }

        // Turns active unit's active bool off
        activeUnit.isActiveUnit = false;
        // Turns off the active unit's active marker
        activeUnit.activeMarker.SetActive(false);
        // Removes the current active unit
        activeUnit = null;
        // Returns highlight markers to their pool
        ResetHighlights();
    }

    // Returns highlight markers to their pool
    public void ResetHighlights()
    {
        // Goes through all highlight markers in highlights array
        foreach (GameObject highlight in highlights)
        {
            // Returns the highlight marker to the pool
            highlight.transform.localPosition = new Vector2(0f, 0f);
            // Clears the highlight marker's eatable unit
            highlight.GetComponent<Highlight>().canEatUnit = null;
        }
    }

    // Moves the unit
    public void MoveUnit(GameObject highlight)
    {
        // Checks if unit is already moving
        if (moving)
        {
            // Ends function if true
            return;
        }

        // Plays the click sound
        GetComponent<AudioSource>().Play();

        // Eats a unit if a unit can be eaten
        highlight.GetComponent<Highlight>().EatUnit();

        // Moves unit to the highlight marker's position
        activeUnit.transform.position = highlight.transform.position;

        // Sets the active card to the opponent's queued card
        FindObjectOfType<CardsManager>().SetNextCard(activeCard);

        // Checks win conditions
        activeUnit.CheckKingBlue();
        activeUnit.CheckKingRed();

        // Declare that the active unit is moving
        moving = true;

        // Transitions to Next Turn
        NextTurn();
    }

    // Transition the turn to the opponent
    void NextTurn()
    {
        // Checks is current player taking his/her turn is Blue
        if (currentTurnColor == "Blue")
        {
            // Changes current turn color to Red
            currentTurnColor = "Red";
            // Deactivates Blue turn text
            blueTurn.SetActive(false);
            // Activates Red turn text
            redTurn.SetActive(true);
        } /* Checks is current player taking his/her turn is Blue (usually means it's Red) */
        else
        {
            // Changes current turn color to Blue
            currentTurnColor = "Blue";
            // Deactivates Red turn text
            redTurn.SetActive(false);
            // Activates Blue turn text
            blueTurn.SetActive(true);
        }

        // Clears the active card
        ClearActiveCard();
        // Clears the active unit
        ClearActiveUnit();

        // Declare that the active unit is no longer moving
        moving = false;
    }

    // Trigger Win
    public void Win(string winColor)
    {
        // Turn on win board
        winBoard.SetActive(true);
        // Turn on win text
        winText.gameObject.SetActive(true);
        // Set win text to declare which player won
        winText.text = winColor + " wins!";
        // Declare that the game is over after short delay
        Invoke("Gameover", .2f);
    }

    // Declares that the game is over
    void Gameover()
    {
        // Sets gameOver bool to true
        gameOver = true;
    }
}
