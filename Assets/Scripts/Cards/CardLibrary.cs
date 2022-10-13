using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardLibrary : MonoBehaviour
{
    // Where cards go when obtained
    public List<Card> Inventory;
    // The current set of cards a player has set for battle
    public List<Card> CurrentSet;

    // Start is called before the first frame update
    void Start()
    {
        GiveDefaultCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GiveDefaultCards()
    {
        for (int i = 0; i < 25; i++)
        {
            Card card = ScriptableObject.CreateInstance("Card001") as Card;
            card.data.CardObj.name = "Card";
            card.data.Player = gameObject;
            card.data.CardObj.transform.SetParent(card.data.Player.transform);
            Inventory.Add(card);
        }
        for (int i = 0; i < 5; i++)
        {
            Card card = ScriptableObject.CreateInstance("Card090") as Card;
            card.data.CardObj.name = "Card";
            card.data.Player = gameObject;
            card.data.CardObj.transform.SetParent(card.data.Player.transform);
            Inventory.Add(card);
        }
        CurrentSet = new List<Card>(Inventory);
    }
}
