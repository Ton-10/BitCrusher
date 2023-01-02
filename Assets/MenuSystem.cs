using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public Text InventoryLimit;
    public Image InventoryBar;
    public GameObject CombatSetContainer;
    public GameObject InventoryContainer;
    public GameObject Player;
    public CardLibrary PlayerLibrary;
    bool opened = false;
    // Start is called before the first frame update
    void Start()
    {
        InventoryLimit = transform.Find("root/CombatSet/Card Num").GetComponent<Text>();
        InventoryBar = transform.Find("root/CombatSet/Bar").GetComponent<Image>();
        CombatSetContainer = transform.Find("root/CombatSet/Base/Container").gameObject;
        InventoryContainer = transform.Find("root/Inventory/Base/Container").gameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerLibrary = Player.GetComponent<CardLibrary>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            Debug.Log("Menu");
            if (!opened)
            {
                opened = true;
                Open();
            }
            else
            {
                opened = false;
                Close();
            }
        }

            // Check if the left mouse button was clicked
            if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject.GetComponent<Linker>() != null)
            {
                GameObject CurrentObject = EventSystem.current.currentSelectedGameObject;
                Card card = CurrentObject.GetComponent<Linker>().cardScript;
                    if (CurrentObject.transform.parent.parent.parent.name == "CombatSet")
                    {
                        // add to inventory and remove from combat set

                        GameObject entry = Instantiate(Resources.Load("CardEntry") as GameObject);
                        entry.transform.Find("Text").GetComponent<Text>().text = card.GetType().ToString();
                        entry.transform.SetParent(InventoryContainer.transform);
                        entry.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        entry.GetComponent<Linker>().cardScript = card;

                        DestroyImmediate(CurrentObject);
                        Debug.Log(CurrentObject + "\n was removed");
                    }
                    else if (CurrentObject.transform.parent.parent.parent.name == "Inventory")
                    {
                        // Add to combat set and remove from inventory

                        GameObject entry = Instantiate(Resources.Load("CardEntry") as GameObject);
                        entry.transform.Find("Text").GetComponent<Text>().text = card.GetType().ToString();
                        entry.transform.SetParent(CombatSetContainer.transform);
                        entry.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        entry.GetComponent<Linker>().cardScript = card;

                        DestroyImmediate(CurrentObject);
                        Debug.Log(CurrentObject + "\n was removed");
                    }
                // Update actual combat set and inventory
                    PlayerLibrary.CurrentSet.Clear();
                    PlayerLibrary.Inventory.Clear();
                foreach (Transform entry in CombatSetContainer.transform)
                    {
                    PlayerLibrary.CurrentSet.Add(entry.GetComponent<Linker>().cardScript); 
                    }
                    foreach (Transform entry in InventoryContainer.transform)
                    {
                    PlayerLibrary.Inventory.Add(entry.GetComponent<Linker>().cardScript);
                    }

                }

            }
        InventoryLimit.text = PlayerLibrary.CurrentSet.Count + "/" + 35;
        InventoryBar.fillAmount = PlayerLibrary.CurrentSet.Count / 35f;
    }
    public void Open()
    {
        gameObject.transform.Find("root").gameObject.SetActive(true);
        gameObject.GetComponent<Image>().enabled = true;
        foreach (Card item in PlayerLibrary.Inventory)
        {
            GameObject entry = Instantiate(Resources.Load("CardEntry") as GameObject);
            entry.transform.Find("Text").GetComponent<Text>().text = item.GetType().ToString();
            entry.transform.SetParent(InventoryContainer.transform);
            entry.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            entry.GetComponent<Linker>().cardScript = item;
        }

        foreach (Card item in PlayerLibrary.CurrentSet)
        {
            GameObject entry = Instantiate(Resources.Load("CardEntry") as GameObject);
            entry.transform.Find("Text").GetComponent<Text>().text = item.GetType().ToString();
            entry.transform.SetParent(CombatSetContainer.transform);
            entry.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            entry.GetComponent<Linker>().cardScript = item;
        }
    }
    public void Close()
    {
        gameObject.transform.Find("root").gameObject.SetActive(false);
        gameObject.GetComponent<Image>().enabled = false;
        foreach (Transform entry in InventoryContainer.transform)
        {
            Destroy(entry.gameObject);
        }
        foreach (Transform entry in CombatSetContainer.transform)
        {
            Destroy(entry.gameObject);
        }
    }
}
