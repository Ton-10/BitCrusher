// NPC Action
using UnityEngine;

public class GiveCard : Action
{
    public override void StartAction()
    {
        base.StartAction();

        Card card = ScriptableObject.CreateInstance("Card001") as Card;
        card.data.CardObj.name = "Card";
        card.data.Player = gameObject;
        card.data.CardObj.transform.SetParent(card.data.Player.transform);

        Player.GetComponent<CardLibrary>().Inventory.Add(card);
    }
}
