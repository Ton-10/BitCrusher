using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageS : MonoBehaviour
{
    public List<DamageS> DamageInstances;
    public List<GameObject> hits;
    public bool damaged;
    public int DamageValue;
    public int Damage
    {
        get { damaged = true; return DamageValue;}
        set { DamageValue = value; }
    }
    
    public enum CardType { SingleStage, MultiStage };
    public void SyncHits(GameObject hit)
    {
        foreach (DamageS damageInstance in DamageInstances)
        {
            damageInstance.hits.Add(hit);
        }
    }
}
