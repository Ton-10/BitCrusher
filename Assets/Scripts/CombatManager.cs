using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool InCombat;
    Vector3 originalPlayerPos, originalCamPos;
    Quaternion originalCamRot;
    GameObject enemies = null;
    public void StartCombat(GameObject enemy)
    {
        InCombat = true;
        enemies = enemy;
        originalPlayerPos = gameObject.transform.Find("Body").transform.position;
        gameObject.GetComponent<MapMovement>().CanMove = false;
        gameObject.GetComponent<CombatMovement>().CanMove = true;
        gameObject.GetComponent<CombatMovement>().enabled = true;
        GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
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
            if (InCombat == true && (!gameObject.GetComponent<Hp>().Alive || !enemies.GetComponent<Hp>().Alive))
            {
                // Combat end
                gameObject.GetComponent<MapMovement>().CanMove = true;
                gameObject.GetComponent<CombatMovement>().CanMove = false;
                gameObject.GetComponent<CombatMovement>().enabled = false;
                gameObject.transform.Find("Body").transform.position = originalPlayerPos;
                GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
                Cam.transform.position = originalCamPos;
                Cam.transform.rotation = originalCamRot;
                
                Destroy(enemies);
            }
        }
    }
}
