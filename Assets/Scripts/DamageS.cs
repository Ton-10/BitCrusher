using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageS : MonoBehaviour
{
    public bool damaged;
    public int DamageValue;
    public int Damage
    {
        get { damaged = true; return DamageValue;}
        set { DamageValue = value; }
    }
    
    public enum CardType { SingleStage, MultiStage };
}
