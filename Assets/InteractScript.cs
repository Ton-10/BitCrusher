using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public bool closeToObject;
    public Action action;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (closeToObject)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("Interacted!");
                action.StartAction();
            }
        }
    }
    void OnTriggerEnter(Collider hit)
    {
        Debug.Log($"{hit.gameObject} Entered Interaction area of {gameObject}");
        if (hit.gameObject.tag == "Player")
        {
            closeToObject = true;
            action.Player = hit.gameObject;
            action.Actor = gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other.gameObject} Left Interaction area of {gameObject}");
        if (other.gameObject.tag == "Player")
        {
            closeToObject = false;
        }
    }
}
