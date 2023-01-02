using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool InCombat;
    Vector3 originalPlayerPos, originalCamPos;
    Quaternion originalCamRot;
    List<GameObject> enemies = new List<GameObject>();
    public void StartCombat(GameObject enemy)
    {
        InCombat = true;
        enemies.Add(enemy);
        originalPlayerPos = gameObject.transform.Find("Body").transform.position;
        gameObject.GetComponent<MapMovement>().CanMove = false;
        gameObject.GetComponent<CombatMovement>().CanMove = true;
        gameObject.GetComponent<CombatMovement>().enabled = true;
        GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
        Cam.GetComponent<FollowCamera>().enabled = false;
        originalCamPos = Cam.transform.position;
        originalCamRot = Cam.transform.rotation;
        Cam.transform.rotation = Quaternion.Euler(0, -90, 0);
        Cam.transform.position = gameObject.GetComponent<CombatMovement>().PlayerField.transform.Find("Center").position + new Vector3(50, 0, 0);
        gameObject.GetComponent<Custom>().enabled = true;
        enemy.GetComponent<MovementController>().CanMove = true;
        enemy.transform.position = 
            gameObject.GetComponent<CombatMovement>().PlayerField.transform.Find("Plane/OverlayE/midRow/Tile (3)").transform.position;
        
    }
    public void Update()
    {
        if (gameObject != null && enemies != null)
        {
            if (InCombat == true && (!gameObject.GetComponent<Hp>().Alive || !CheckEnemies()))
            {
                InCombat = false;
                // Combat end
                Debug.Log("Combat End");
                gameObject.GetComponent<MapMovement>().CanMove = true;
                gameObject.GetComponent<CombatMovement>().CanMove = false;
                gameObject.GetComponent<CombatMovement>().enabled = false;
                gameObject.transform.Find("Body").transform.position = originalPlayerPos;
                gameObject.GetComponent<Custom>().HideCust();
                gameObject.GetComponent<Custom>().enabled = false;
                GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");    
                Cam.transform.position = originalCamPos;
                Cam.transform.rotation = originalCamRot;
                Cam.GetComponent<FollowCamera>().enabled = true;
                // Show win screen
                float Time = gameObject.GetComponent<Custom>().TotalTime;
                float grade = 1000 / Time;
                //If done within ~7 seconds give card
                if(grade >= 140)
                {
                    giveCard();
                }
            }
        }
    }
    public bool CheckEnemies()
    {
        bool areDead = false;
        foreach (GameObject enemy in enemies)
        {
            Hp hp = enemy.GetComponent<Hp>();
            bool isAlive = hp.Alive;
            // If not alive, send kill command
            if (!isAlive)
            {
                hp.Kill();
            }
            areDead = areDead == false ? isAlive : true;
        }
        return areDead;
    }
    void giveCard()
    {
        int enemyNumber = Random.Range(0, enemies.Count);

        Card card = Instantiate(enemies[enemyNumber].GetComponent<MovementController>().Cards[0]);
        card.data.CardObj.name = "Card";
        card.data.Player = gameObject;
        card.data.CardObj.transform.SetParent(card.data.Player.transform);
        gameObject.GetComponent<CardLibrary>().Inventory.Add(card);
    }
}
