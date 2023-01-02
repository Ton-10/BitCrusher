using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    public CardData data;
    virtual public bool Hit{ get; set; }
    protected string CardDescription;
    public virtual IEnumerator Activate()
    {
        Debug.Log($"Card: {data.Id} Activated");
        yield return null;
    }

    public struct CardData
    {
        public int Attack, RangeX, RangeY, Id, CooldownFrames, stage;
        public bool IsNext, IsSelected, IsUsedUp;
        public GameObject Indicator;
        public GameObject Player, IndicatorField, CurIndicator, CardObj;
        

        public CardData(int attack, int rangeX, int rangeY, int id, int cooldown)
        {
            //Set card data
            Attack = attack;
            RangeX = rangeX;
            RangeY = rangeY;
            Id = id;
            CooldownFrames = cooldown;
            //Set defaults
            stage = 0;
            IsNext = false;
            IsSelected = false;
            IsUsedUp = false;
            Indicator = null;
            Player = null;
            IndicatorField = null;
            CurIndicator = null;
            CardObj = null;
        }
    }
}
