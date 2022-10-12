using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool CanMove, IsMoving;
    public GameObject PlayerField;
    public GameObject Overlay;
    public List<List<GameObject>> GameTiles = new List<List<GameObject>>();
    public int PosX, PosY, DirX, DirY;
    public float MovementSpeed;
    public List<Card> Cards;
    Animator anim;

    public virtual void Start()
    {
        //anim = transform.Find("Body").gameObject.GetComponent<Animator>();
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

        PosX = 1;
        PosY = 1;
    }

    public void PlayMoveAnimation()
    {
        anim.SetTrigger("Move");
    }
    public void AddTiles(List<GameObject> tiles)
    {
        Debug.Log("Added Tiles");
        GameTiles.Add(tiles);
    }
}
