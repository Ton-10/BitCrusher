using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject PlayerField;
    public Hp PlrHpScript;
    public GameObject Overlay;
    public List<List<GameObject>> GameTiles = new List<List<GameObject>>();
    public int PlayerX;
    public int PlayerY;

    public bool CanMove;
    private bool moveCooldown = false;
    private bool r, l, u, d;

    private float inputBuffer = 0f;
    private float deadZone = 0f;
    private float x, y;
    private float moveCooldownFrames, WaitTime;

    private int frames = 5;
    private Vector3 initialPos;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.Find("Body").gameObject.GetComponent<Animator>();
        List<GameObject> tiles = new List<GameObject>();
        foreach (Transform tile in Overlay.transform.Find("frontRow"))
        {
            tiles.Add(tile.gameObject);
        }
        AddTiles(tiles);
        tiles = new List<GameObject>();
        foreach (Transform tile in Overlay.transform.Find("midRow"))
        {
            tiles.Add(tile.gameObject);
        }
        AddTiles(tiles);
        tiles = new List<GameObject>();
        foreach (Transform tile in Overlay.transform.Find("backRow"))
        {
            tiles.Add(tile.gameObject);
        }
        AddTiles(tiles);
        tiles = new List<GameObject>();

        moveCooldownFrames = frames;
        PlayerX = 1;
        PlayerY = 1;
        UpdatePosition();
        PlrHpScript = GetComponent<Hp>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateIndo = anim.GetCurrentAnimatorStateInfo(0);
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
                        if(PlayerY > 0)
                        {
                            PlayerY--;
                            UpdatePosition();
                        }
                    }
                    // Down
                    if (Input.GetAxisRaw("Vertical") < -inputBuffer && !moveCooldown && !d)
                    {
                        d = !d;
                        moveCooldown = !moveCooldown;
                        if (PlayerY < 2)
                        {
                            PlayerY++;
                            UpdatePosition();
                        }
                    }
                    // Left
                    if (Input.GetAxisRaw("Horizontal") < -inputBuffer && !moveCooldown && !l)
                    {
                        l = !l;
                        moveCooldown = !moveCooldown;
                        if (PlayerX < 2)
                        {
                            PlayerX++;
                            UpdatePosition();
                        }
                    }
                    // Right
                    if (Input.GetAxisRaw("Horizontal") > inputBuffer && !moveCooldown && !r)
                    {
                        r = !r;
                        moveCooldown = !moveCooldown;
                        if (PlayerX > 0)
                        {
                            PlayerX--;
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
        transform.position = GameTiles[PlayerX][PlayerY].transform.position + new Vector3(1,0,0);
    }
    public void WaitForTime(float t)
    {
        WaitTime = Time.time+t;
    }
    public void PlayMoveAnimation()
    {
        anim.SetTrigger("Move");
    }
    public void AddTiles(List<GameObject> tiles)
    {
        GameTiles.Add(tiles);
    }
}
