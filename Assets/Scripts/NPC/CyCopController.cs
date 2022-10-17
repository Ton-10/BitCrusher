using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyCopController : MovementController
{
    bool AttackQueued;
    Card card;
    public int movesTillAttack = 3;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        card = ScriptableObject.CreateInstance("Card050") as Card;
        card.data.CardObj.name = "Card";
        card.data.Player = gameObject;
        card.data.CardObj.transform.SetParent(card.data.Player.transform);
        card.data.IndicatorField = PlayerField.transform.Find("Plane/Indicators").gameObject;
        Cards.Add(card);
        MovementSpeed = 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.right);
        if (Physics.Raycast(transform.position, fwd, out RaycastHit hit, 100) && AttackQueued == false && movesTillAttack == 0)
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                Attack();
            }
        }

        if (CanMove)
        {
            if (!IsMoving)
            {
                if (PosY == 0)
                {
                    // Move down 1
                    IsMoving = true;
                    DirY = 1;
                }
                else if (PosY == 1)
                {   
                    if(DirY == 0)
                    {
                        // Pick a random DirY
                        DirY = Random.Range(0, 1) == 1 ? 1 : -1;
                    }
                    IsMoving = true;
                }
                else if (PosY == 2)
                {
                    // Move up
                    IsMoving = true;
                    DirY = -1;

                }
            }
            MoveTo(0, DirY);
        }
        if (AttackQueued)
        {
            Attack();
        }
    }
    public void MoveTo(int X, int Y)
    {
        // Logic for picking destination
        X = X == 0 ? PosX : PosX + X;
        Y = Y == 0 ? PosY : PosY + Y;
        if (Vector3.Distance(transform.position, GameTiles[X][Y].transform.position + new Vector3(2, 0, 0)) > 0.05)
        {
            Vector3 destination = GameTiles[X][Y].transform.position + new Vector3(2,0,0);
            // Move to destination 
            transform.position = Vector3.MoveTowards(transform.position, destination, MovementSpeed * Time.deltaTime);
        }
        else
        {
            IsMoving = false;
            PosX = X;
            PosY = Y;
            movesTillAttack = movesTillAttack - 1 > 0 ? movesTillAttack - 1 : 0;
        }
    }
    void Attack()
    {
        if (IsMoving)
        {
            AttackQueued = true;
            // wait for finish moving
        }
        else
        {
            AttackQueued = false;
            CanMove = false;
            movesTillAttack = 3;
            StartCoroutine(card.Activate());

        }
        
    }
}
