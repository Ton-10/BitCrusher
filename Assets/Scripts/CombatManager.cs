using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool InCombat;
    Vector3 originalPos;
    GameObject enemies = null;
    public void StartCombat(GameObject enemy)
    {
        InCombat = true;
        enemies = enemy;
        originalPos = gameObject.transform.Find("Body").transform.position;
        gameObject.GetComponent<MapMovement>().CanMove = false;
        gameObject.GetComponent<CombatMovement>().CanMove = true;
        gameObject.GetComponent<CombatMovement>().enabled = true;
        GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
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
            if (InCombat == true && (!gameObject.GetComponent<Hp>().Alive || !enemies.GetComponent<Hp>().Alive))
            {
                gameObject.GetComponent<MapMovement>().CanMove = true;
                gameObject.GetComponent<CombatMovement>().CanMove = false;
                gameObject.GetComponent<CombatMovement>().enabled = false;
                gameObject.transform.Find("Body").transform.position = originalPos;
                GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
                Cam.transform.rotation = Quaternion.Euler(45, -90, 0);
                Cam.transform.position = gameObject.transform.Find("Body").transform.position + new Vector3(100, 0, 0);
                Destroy(enemies);
            }
        }
    }
}
