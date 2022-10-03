using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardLibrary : MonoBehaviour
{
    // Where cards go when obtained
    public List<Card> Inventory;
    // The current set of cards a player has set for battle
    public List<Card> BattleCards;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            Card card = ScriptableObject.CreateInstance("Card001") as Card;
            card.data.CardObj.name = "Card";
            card.data.Player = gameObject;
            card.data.CardObj.transform.SetParent(card.data.Player.transform);
            Inventory.Add(card);
        }
        for (int i = 0; i < 10; i++)
        {
            Card card = ScriptableObject.CreateInstance("Card090") as Card;
            card.data.CardObj.name = "Card";
            card.data.Player = gameObject;
            card.data.CardObj.transform.SetParent(card.data.Player.transform);
            card.data.CardObj.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.8f);
            Inventory.Add(card);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
