using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card001 : Card
{
    static readonly string cardPrefabPath = "Card";
    static readonly string Indicator = "Indicator";

    public Card001()
    {
        data = new CardData(60, 2, 1, int.Parse(GetType().ToString().Split('d')[1]), 5);
        
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
        data.Player.transform.Find("Body").gameObject.GetComponent<Animator>().SetTrigger("Shoot");
        List<DamageS> scripts = new List<DamageS>();
        for (int i = 1; i < data.RangeX + 1; i++)
        {
            data.CurIndicator = Instantiate(data.Indicator, data.Player.transform.position, Quaternion.Euler(0, -90, 0));
            data.CurIndicator.name = "Indicator";
            data.CurIndicator.transform.parent = data.IndicatorField.transform;
            scripts.Add(data.CurIndicator.GetComponent<DamageS>());
            data.CurIndicator.GetComponent<DamageS>().Damage = data.Attack;
            data.CurIndicator.transform.localPosition = 
                new Vector3(
                    data.CurIndicator.transform.localPosition.x, 
                    data.CurIndicator.transform.localPosition.y, 
                    data.CurIndicator.transform.localPosition.z + 0.34f * i);
            data.CurIndicator.transform.localScale = new Vector3(0.04f, 0.04f, 1);
            Destroy(data.CurIndicator, data.CooldownFrames / 60f);
        }
        // Sync damage instances
        foreach (DamageS script in scripts)
        {
            script.DamageInstances = scripts;
        }
    }
}
