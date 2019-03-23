using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitColor;

    public bool isActiveUnit = false;

    GameController gameController;

    public GameObject activeMarker;

    public bool isKing = false;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (unitColor == gameController.currentTurnColor)
        {
            collision.transform.localPosition = new Vector2(0f, 0f);
        } else
        {
            collision.GetComponent<Highlight>().canEatUnit = this;
        }
    }

    public void MakeActiveUnit()
    {
        if (unitColor != gameController.currentTurnColor)
        {
            return;
        }

        if (isActiveUnit)
        {
            FindObjectOfType<GameController>().activeUnit = null;
            FindObjectOfType<GameController>().ResetHighlights();
            isActiveUnit = false;
            activeMarker.SetActive(false);
            return;
        }

        gameController.ClearActiveUnit();

        gameController.activeUnit = this;
        isActiveUnit = true;
        activeMarker.SetActive(true);
        gameController.ShowAvailableMoves();
    }

    
    public void CheckKingBlue()
    {
        Vector3 redThrone = new Vector3(2.5f, 4.5f);

        if (isKing && unitColor == "Blue" && transform.position == redThrone)
        {
            FindObjectOfType<GameController>().Win(unitColor);
        }
    }

    public void CheckKingRed()
    {
        Vector3 blueThrone = new Vector3(2.5f, 0.5f);

        if (isKing && unitColor == "Red" && transform.position == blueThrone)
        {
            FindObjectOfType<GameController>().Win(unitColor);
        }
    }
}
