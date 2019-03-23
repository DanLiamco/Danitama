using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public Card nextRedCard;
    public Card nextBlueCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNextCard(Card activeCard)
    {
        if(activeCard.cardColor == "Blue")
        {
            if (nextBlueCard)
            {
                nextBlueCard.transform.position = activeCard.transform.position;
                nextBlueCard.cardColor = "Blue";
            }

            nextRedCard = activeCard;
            activeCard.transform.position = new Vector2(2.5f, 7f);

            Quaternion newRotation = Quaternion.Euler(0f, 0f, 180f);
            nextRedCard.transform.rotation = newRotation;

        } else
        {
            if (nextRedCard)
            {
                nextRedCard.transform.position = activeCard.transform.position;
                nextRedCard.cardColor = "Red";
            }

            nextBlueCard = activeCard;
            activeCard.transform.position = new Vector2(2.5f, -2f);

            Quaternion newRotation = Quaternion.Euler(0f, 0f, 0f);
            nextBlueCard.transform.rotation = newRotation;
        }
    }
}
