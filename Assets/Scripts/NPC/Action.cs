using UnityEngine;

public class Action : MonoBehaviour
{
    public GameObject Player, Actor;

    public virtual void StartAction()
    {
        Debug.Log("Started action!");
    }
}
