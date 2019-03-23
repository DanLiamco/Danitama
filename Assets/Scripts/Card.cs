using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public string cardColor;

    public Vector2[] movementPoints;

    [SerializeField]
    string name;

    [SerializeField]
    Text cardText;

    public bool isActiveCard = false;

    GameController gameController;

    public GameObject activeMarker;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeActiveCard()
    {
        if (cardColor != gameController.currentTurnColor)
        {
            return;
        }

        if (isActiveCard)
        {
            FindObjectOfType<GameController>().activeCard = null;
            FindObjectOfType<GameController>().ResetHighlights();
            isActiveCard = false;
            activeMarker.SetActive(false);
            return;
        }

        gameController.ClearActiveCard();

        gameController.activeCard = this;
        isActiveCard = true;
        activeMarker.SetActive(true);
        gameController.ShowAvailableMoves();
    }
}
