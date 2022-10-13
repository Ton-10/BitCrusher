using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool InCombat;

    public void StartCombat(GameObject enemy)
    {
        InCombat = true;
        gameObject.GetComponent<MapMovement>().CanMove = false;
        gameObject.GetComponent<CombatMovement>().CanMove = true;
        gameObject.GetComponent<CombatMovement>().enabled = true;
        GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
        Cam.transform.rotation = Quaternion.Euler(0, -90, 0);
        Cam.transform.position = gameObject.GetComponent<CombatMovement>().PlayerField.transform.Find("Center").position + new Vector3(50, 0, 0);
        gameObject.GetComponent<Custom>().enabled = true;
        enemy.GetComponent<EnemyController>().CanMove = true;
        // Add enemy to grid

    }
}
