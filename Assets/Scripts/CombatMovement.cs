using System.Collections.Generic;
using UnityEngine;

public class CombatMovement : MovementController
{
    public Hp PlrHpScript;
    private bool moveCooldown = false;
    private bool r, l, u, d;

    private float inputBuffer = 0f;
    private float deadZone = 0f;
    private float x, y;
    private float moveCooldownFrames;

    private int frames = 5;
    private Vector3 initialPos;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        anim = transform.Find("Body").gameObject.GetComponent<Animator>();
        moveCooldownFrames = frames;
        UpdatePosition();
        PlrHpScript = GetComponent<Hp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WaitTime < Time.time)
        {
            if (Input.anyKeyDown)
            {
                // Directions are kinda weird, script takes all the tiles and puts them in a list in order and 
                // The PlayerX and Y is just tracking where in the index the tile the player is on
                if (CanMove)
                {
                    // Up
                    if (Input.GetAxisRaw("Vertical") > inputBuffer && !moveCooldown && !u)
                    {
                        u = !u;
                        moveCooldown = !moveCooldown;
                        if(PosY > 0)
                        {
                            PosY--;
                            UpdatePosition();
                        }
                    }
                    // Down
                    if (Input.GetAxisRaw("Vertical") < -inputBuffer && !moveCooldown && !d)
                    {
                        d = !d;
                        moveCooldown = !moveCooldown;
                        if (PosY < 2)
                        {
                            PosY++;
                            UpdatePosition();
                        }
                    }
                    // Left
                    if (Input.GetAxisRaw("Horizontal") < -inputBuffer && !moveCooldown && !l)
                    {
                        l = !l;
                        moveCooldown = !moveCooldown;
                        if (PosX < 2)
                        {
                            PosX++;
                            UpdatePosition();
                        }
                    }
                    // Right
                    if (Input.GetAxisRaw("Horizontal") > inputBuffer && !moveCooldown && !r)
                    {
                        r = !r;
                        moveCooldown = !moveCooldown;
                        if (PosX > 0)
                        {
                            PosX--;
                            UpdatePosition();
                        }
                    }
                }
            }
        }
        // If input at deadzone then reset directional bools
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == deadZone)
        {
            r = false;
            l = false;
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == deadZone)
        {
            u = false;
            d = false;
        }

        if (moveCooldown)
        {
            moveCooldownFrames--;
            if (moveCooldownFrames <= 0)
            {
                moveCooldown = !moveCooldown;
                moveCooldownFrames = frames;
            }

        }

    }
    public void UpdatePosition()
    {
        PlayMoveAnimation();
        transform.position = GameTiles[PosX][PosY].transform.position + new Vector3(1,2,0);
    }
}
