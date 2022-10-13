using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageS : MonoBehaviour
{
    public List<DamageS> DamageInstances;
    public List<GameObject> hits;
    public bool damaged;
    public int DamageValue;
    public Card creator;
    public int Damage
    {
        get { damaged = true; return DamageValue;}
        set { DamageValue = value; }
    }
    
    public enum CardType { SingleStage, MultiStage };
    // Part of the logic to prevent a move from hitting multiple times on one enemy
    public void SyncHits(GameObject hit)
    {
        foreach (DamageS damageInstance in DamageInstances)
        {
            damageInstance.hits.Add(hit);
        }
    }
    void OnTriggerEnter(Collider hit)
    {
        if ((hit.gameObject.CompareTag("Player") || hit.gameObject.CompareTag("NPC")) && !damaged && !hits.Contains(hit.gameObject) && creator != null)
        {
            SyncHits(hit.gameObject);
            hit.gameObject.GetComponent<Hp>().HP -= Damage;
            Debug.Log(hit.gameObject.tag);
            creator.Hit = true;
        }
    }
}
