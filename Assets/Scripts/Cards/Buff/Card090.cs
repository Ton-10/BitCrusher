using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card090 : Card
{
    static readonly string cardPrefabPath = "Card";
    static readonly string Indicator = "Indicator";

    public Card090()
    {
        data = new CardData(50, 2, 1, int.Parse(GetType().ToString().Split('d')[1]), 5);
    }
    public void OnEnable()
    {
        data.CardObj = Instantiate(Resources.Load(cardPrefabPath) as GameObject);
        data.CardObj.GetComponent<Linker>().cardScript = this;
        data.CardObj.gameObject.transform.Find("Attack").GetComponent<Text>().text = data.Attack.ToString();
        data.Indicator = Instantiate(Resources.Load(Indicator) as GameObject);
    }
    public override void Activate()
    {
        data.IsUsedUp = true;
        data.Player.GetComponent<CombatMovement>().WaitForTime(data.CooldownFrames / 60f);
        data.Player.transform.Find("Body").gameObject.GetComponent<Animator>().SetTrigger("Move");
    }
}
