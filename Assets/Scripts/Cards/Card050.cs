using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card050 : Card
{
    static readonly string cardPrefabPath = "Card";
    static readonly string Indicator = "Indicator";
    List<DamageS> scripts = new List<DamageS>();
    GameObject projecile;
    override public bool Hit
    {
        get {
            return Hit;
        }
        set
        {
            if (projecile != null)
            {
                GameObject target = scripts[0].hits[scripts[0].hits.Count-1].gameObject;
                Destroy(projecile);
                target.GetComponent<CombatMovement>().CanMove = false;
            }

        }
    }

    public Card050()
    {
        Debug.Log(int.Parse(GetType().ToString().Split('d')[1]));
        data = new CardData(40, 3, 1, int.Parse(GetType().ToString().Split('d')[1]), 5);
        
    }
    public void OnEnable()
    {
        data.CardObj = Instantiate(Resources.Load(cardPrefabPath) as GameObject);
        data.CardObj.GetComponent<Linker>().cardScript = this;
        data.CardObj.gameObject.transform.Find("Attack").GetComponent<Text>().text = data.Attack.ToString();
        data.Indicator = Instantiate(Resources.Load(Indicator) as GameObject);
        data.CardObj.GetComponent<Image>().color = new Color(1f, 0.6f, 0.6f);
    }
    public override IEnumerator Activate()
    {
        data.IsUsedUp = true;
        if (data.Player.CompareTag("Player"))
        {
            data.Player.GetComponent<CombatMovement>().WaitForTime(data.CooldownFrames / 60f);
        }
        //data.Player.transform.Find("Body").gameObject.GetComponent<Animator>().SetTrigger("Shoot");
        
        float indicatorPlacementDistance = 0.34f;
        if(data.Player.CompareTag("NPC")){
            indicatorPlacementDistance = -0.34f;
        }

        //start and shoot projectile
        projecile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        projecile.GetComponent<BoxCollider>().isTrigger = true;
        projecile.transform.position = data.Player.transform.position;
        projecile.transform.SetParent(data.Player.transform);
        Rigidbody rb = projecile.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = new Vector3(0, 0, -1) * 60;
        Destroy(projecile, 1.5f);

        for (int i = 1; i < data.RangeX + 1; i++)
        {
            data.CurIndicator = Instantiate(data.Indicator, data.Player.transform.position, Quaternion.Euler(0, -90, 0));
            data.CurIndicator.name = "Indicator";
            data.CurIndicator.transform.parent = data.IndicatorField.transform;
            scripts.Add(data.CurIndicator.GetComponent<DamageS>());
            data.CurIndicator.GetComponent<DamageS>().creator = this;
            data.CurIndicator.GetComponent<DamageS>().Damage = data.Attack;
            data.CurIndicator.transform.localPosition = 
                new Vector3(
                    data.CurIndicator.transform.localPosition.x, 
                    data.CurIndicator.transform.localPosition.y, 
                    data.CurIndicator.transform.localPosition.z + indicatorPlacementDistance * i);
            data.CurIndicator.transform.localScale = new Vector3(0.04f, 0.04f, 1);
            Destroy(data.CurIndicator, 0.15f);
            // Sync damage instances
            foreach (DamageS script in scripts)
            {
                script.DamageInstances = scripts;
            }
            yield return new WaitForSeconds(0.5f / data.RangeX);
        }
        if (data.Player.CompareTag("NPC"))
        {
            data.Player.GetComponent<EnemyController>().CanMove = true;
        }
    }
}
