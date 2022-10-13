using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    public List<GameObject> CardQueue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (transform.GetComponent<CombatMovement>().CanMove)
            {
                if (CardQueue.Count > 0)
                {
                    StartCoroutine(CardQueue[0].GetComponent<Linker>().cardScript.Activate());
                    CardQueue.RemoveAt(0);
                }
            }
        }  
    }
}
