using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BackInItemPanel : MonoBehaviour,IPointerDownHandler
{
    public Sprite addItem;

    public GameObject[] item_slots;

    public ItemBuyOrErrorMessage itemBuyOrErrorMessage;
    public void OnPointerDown(PointerEventData eventData)
    {
        //deselcting all the items
        int count_increment=0;
        var CoinCount=GameObject.Find("Coin").GetComponent<TextMeshProUGUI>();
        var temp_list_of_selected_items=GameManager.instance.listOfSelectedItems;
        for(int i=0;i<temp_list_of_selected_items.Count;i++)
        {
            count_increment+=temp_list_of_selected_items[i].quantity*temp_list_of_selected_items[i].cost;
        }
        GameManager.instance.CoinCount+=count_increment;
        CoinCount.text=""+GameManager.instance.CoinCount;
        GameManager.instance.listOfSelectedItems.Clear();

        //clearing the slots in the panel
        for(int i=0;i<item_slots.Length;i++)
        {
            item_slots[i].GetComponent<Image>().sprite=addItem;
            item_slots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text="";
            itemBuyOrErrorMessage.item_slots[i].name="";
            itemBuyOrErrorMessage.item_slots[i].used=false;
        }
        

        InSceneManagerInMainMenu.instance.toggleLevelItemSelectionPanel(false,0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
