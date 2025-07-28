using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[System.Serializable]
public struct info_about_slot
{
    public GameObject item_slot;

    public bool used;

    public string name;
}
public class ItemBuyOrErrorMessage : MonoBehaviour
{
    [HideInInspector]public ItemName temp_name;

    [HideInInspector]public Sprite temp_icon;

    [HideInInspector]public int temp_cost;

    [HideInInspector]public int temp_max_allowed;

    public TextMeshProUGUI title,message,positive,negative;

    public info_about_slot[] item_slots;
    public Color positiveColor,NegativeColor;
    public Image coin;
    public void ShowBuyOrNotSegment(int cost,ItemName item_name,Sprite icon,int max_allowed)
    {
        gameObject.SetActive(true);
        temp_name=item_name;
        temp_cost=cost;
        temp_icon=icon;
        temp_max_allowed=max_allowed;

        coin.gameObject.SetActive(true);
        title.text="Pay Up!!";
        title.color=positiveColor;
        message.text=""+item_name+" will cost you "+cost+"c.\nProceed to buy??";
        positive.text="Buy "+cost;
        positive.gameObject.SetActive(true);
        negative.text="Cancel";

    }
    public void ShowErrorSegment(string item_name,int cost,int unlocks_at)
    {
        gameObject.SetActive(true);
        coin.gameObject.SetActive(false);
        title.text="Hold it for a sec!!";
        title.color=NegativeColor;
        message.text=""+item_name+" unlocks at Level "+unlocks_at+" you cannot buy it now!!";
        negative.text="Ok";
        positive.gameObject.SetActive(false);

    }
}
