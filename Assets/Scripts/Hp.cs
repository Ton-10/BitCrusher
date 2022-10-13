using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    public bool Alive;
    private int HitPoints;
    private int HpUIOffest = 90;

    public int HP
    {
        get { return HitPoints; }
        set { HitPoints = value; UpdateHpUI(); }
    }
    public GameObject HpUI;

    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        Alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0 && Alive == true)
        {
            Debug.Log("dead");
            Alive = false;
        }
    }
    public void UpdateHpUI()
    {
        HpUI.GetComponent<Text>().text = "" + HP;
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position) + new Vector3(0,HpUIOffest,0);
        HpUI.transform.position = pos;
    }
}
