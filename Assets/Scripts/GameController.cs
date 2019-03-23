using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Card activeCard;
    public Unit activeUnit;

    float flipSign = 1f;

    [SerializeField]
    GameObject[] highlights;

    public string currentTurnColor;

    [SerializeField]
    Text winText;

    [SerializeField]
    GameObject uiObject;

    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowAvailableMoves()
    {
        if(!activeCard || !activeUnit)
        {
            ResetHighlights();
            return;
        }

        if (activeUnit.unitColor == "Blue")
        {
            flipSign = 1f;
        } else
        {
            flipSign = -1f;
        }

        for (int i = 0; i < activeCard.movementPoints.Length; i++)
        {
            float targetX = activeUnit.transform.position.x + activeCard.movementPoints[i].x * flipSign;
            float targetY = activeUnit.transform.position.y + activeCard.movementPoints[i].y * flipSign;

            if (targetX < 0f || targetX > 5f)
            {
                continue;
            }

            if (targetY < 0f || targetY > 5f)
            {
                continue;
            }

            highlights[i].transform.position = new Vector2(activeUnit.transform.position.x, activeUnit.transform.position.y) + activeCard.movementPoints[i] * flipSign;
        }
    }

    public void ClearActiveCard()
    {
        if (!activeCard)
        {
            return;
        }

        activeCard.isActiveCard = false;
        activeCard.activeMarker.SetActive(false);
        activeCard = null;
        ResetHighlights();
    }

    public void ClearActiveUnit()
    {
        if (!activeUnit)
        {
            return;
        }

        activeUnit.isActiveUnit = false;
        activeUnit.activeMarker.SetActive(false);
        activeUnit = null;
        ResetHighlights();
    }

    public void ResetHighlights()
    {
        foreach (GameObject highlight in highlights)
        {
            highlight.transform.localPosition = new Vector2(0f, 0f);
            highlight.GetComponent<Highlight>().canEatUnit = null;
        }
    }

    public void MoveUnit(GameObject highlight)
    {
        if (moving)
        {
            return;
        }

        highlight.GetComponent<Highlight>().EatUnit();

        activeUnit.transform.position = highlight.transform.position;
        FindObjectOfType<CardsManager>().SetNextCard(activeCard);

        activeUnit.CheckKingBlue();
        activeUnit.CheckKingRed();

        moving = true;

        NextTurn();
    }

    void NextTurn()
    {
        if (currentTurnColor == "Blue")
        {
            currentTurnColor = "Red";
        } else
        {
            currentTurnColor = "Blue";
        }

        ClearActiveCard();
        ClearActiveUnit();

        moving = false;
    }

    public void Win(string winColor)
    {
        uiObject.SetActive(true);
        winText.gameObject.SetActive(true);
        winText.text = winColor + " wins!";
    }
}
