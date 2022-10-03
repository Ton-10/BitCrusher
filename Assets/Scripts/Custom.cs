using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    public float CustomMaxTime,CustomTime;
    public GameObject CustomGUI, cardUI, CardBase, Indicators;
    private List<GameObject> cardQueue;
    private List<Card> playerCards;
    private Button butto;
    private bool cust = true;
    private Image Bar;
    Color InitialColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        cardQueue = transform.GetComponent<CardSelect>().CardQueue;
        playerCards = gameObject.GetComponent<CardLibrary>().Inventory;
        Bar = CustomGUI.transform.Find("Bar").GetComponent<Image>();
        Bar.fillAmount = 1;
        CustomTime = CustomMaxTime;
        transform.GetComponent<Movement>().CanMove = true;
        cardUI = CustomGUI.transform.Find("CardUI").gameObject;
        butto = cardUI.transform.Find("Button").GetComponent<Button>();
        butto.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        cardUI.SetActive(false);
        transform.GetComponent<Movement>().CanMove = true;
        cust = true;
     }
    // Update is called once per frame
    void Update()
    {
        if (cust)
        {
            if(Bar.fillAmount >= 0)
            {
                CustomTime -= Time.deltaTime;
                Bar.fillAmount = CustomTime / CustomMaxTime;
            }
        }
        if (Input.GetButton("Cust"))
        {
            if (cust && Bar.fillAmount <= 0)
            {
                if (cardUI.transform.Find("Card") != null)
                {
                    foreach (Transform item in cardUI.transform)
                    {
                        if (item.gameObject.name == "Card")
                        {
                            Destroy(item.gameObject);
                        }
                    }
                }
                cust = false;
                Bar.fillAmount = 1;
                CustomTime = CustomMaxTime;
                transform.GetComponent<Movement>().CanMove = false;
                cardUI.SetActive(true);
                for (int i = 0; i < 2; i++)
                {
                    for (int ii = 0; ii < 3; ii++)
                    {
                        // fill in cards
                        int cardPick = Random.Range(0, playerCards.Count-1);
                        Card card = playerCards[cardPick];
                        playerCards.RemoveAt(cardPick);
                        card.data.CardObj.transform.SetParent(cardUI.transform);

                        card.data.CardObj.transform.localPosition = new Vector3(-260 + (260 * ii), 150 - (300 * i), 0);
                        card.data.IndicatorField = Indicators;
                    }
                }
            }
        }
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse was clicked over a UI element
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject.CompareTag("Card"))
                {
                    GameObject Target = EventSystem.current.currentSelectedGameObject;
                    Card CurCardScript = Target.GetComponent<Linker>().cardScript;
                    if (cardQueue.Count == 0 || CurCardScript.data.Id == cardQueue[0].GetComponent<Linker>().cardScript.data.Id || CurCardScript.data.Id == 0)
                    {
                        if (!CurCardScript.data.IsSelected)
                        {
                            InitialColor = Target.GetComponent<Image>().color;
                            cardQueue.Add(Target);
                            Target.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
                            CurCardScript.data.IsSelected = true;
                        }
                        else
                        {
                            cardQueue.Remove(Target);
                            Target.GetComponent<Image>().color = InitialColor;
                            CurCardScript.data.IsSelected = false;
                        }
                    }
                }
            } 
        }
    }
}
