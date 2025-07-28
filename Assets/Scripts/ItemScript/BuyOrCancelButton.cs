using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public enum option_type
{
    buy,
    cancel
}
public class BuyOrCancelButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public option_type type;

    public GameObject cantAddMore;

    public ItemBuyOrErrorMessage itemBuyOrErrorMessage;
    public void OnPointerDown(PointerEventData eventData)
    {
        
        if(type==option_type.buy)
        {
            var CoinCount=GameObject.Find("Coin").GetComponent<TextMeshProUGUI>();
            
            string buy=GetComponent<TextMeshProUGUI>().text;
            string[] part=buy.Split(' ');
            int cost=(int)double.Parse(part[1]);
            
            if(cost<=GameManager.instance.CoinCount)
            {
                var temp_list_selectedItems=GameManager.instance.listOfSelectedItems;    
                if(temp_list_selectedItems.Count==3)
                {
                    var flag=0;
                    for(int i=0;i<3;i++)
                    {
                        if(temp_list_selectedItems[i].name==itemBuyOrErrorMessage.temp_name)
                        {
                            flag=1;
                            if(temp_list_selectedItems[i].quantity<itemBuyOrErrorMessage.temp_max_allowed)
                            {
                                //updating the quantity here of the already added object
                                var t=temp_list_selectedItems[i];
                                t.quantity++;
                                temp_list_selectedItems[i]=t;
                            
                                //update the icon here

                                //updating the coin amount here
                                GameManager.instance.CoinCount-=cost;
                                CoinCount.text=""+GameManager.instance.CoinCount;
                                
                                UpdateThePanelSlots(itemBuyOrErrorMessage.temp_name,itemBuyOrErrorMessage.temp_icon);
                                cantAddMore.gameObject.SetActive(false);
                                transform.parent.gameObject.SetActive(false);
                                AudioManager.instance.BuyOptionSound();
                                
                            }
                            else
                            {
                                cantAddMore.gameObject.SetActive(false);
                                cantAddMore.gameObject.SetActive(true);
                            }
                            break;
                        }
                    }
                    if(flag==0)
                    {
                        cantAddMore.gameObject.SetActive(false);
                        cantAddMore.gameObject.SetActive(true);

                    }
                }
                else
                {
                    var flag=0;
                    for(int i=0;i<temp_list_selectedItems.Count;i++)
                    {
                        
                        if(temp_list_selectedItems[i].name==itemBuyOrErrorMessage.temp_name)
                        {
                            flag=1;
                            if(temp_list_selectedItems[i].quantity<itemBuyOrErrorMessage.temp_max_allowed)
                            {
                                //updating the quantity here of the already added object
                                var t=temp_list_selectedItems[i];
                                t.quantity++;
                                temp_list_selectedItems[i]=t;
                                //update the icon here

                                //updating the coin amount here
                                GameManager.instance.CoinCount-=cost;
                                CoinCount.text=""+GameManager.instance.CoinCount;

                                //updating the panel slots icon
                                UpdateThePanelSlots(itemBuyOrErrorMessage.temp_name,itemBuyOrErrorMessage.temp_icon);
                                
                                cantAddMore.gameObject.SetActive(false);
                                transform.parent.gameObject.SetActive(false);
                                AudioManager.instance.BuyOptionSound();
                            }
                            else
                            {
                                cantAddMore.gameObject.SetActive(false);
                                cantAddMore.gameObject.SetActive(true);
                            }
                            break;
                        }
                    }
                    if(flag!=1)
                    {
                        //adding a new item
                        selectedItemInfo temp=new selectedItemInfo();
                        temp.name=itemBuyOrErrorMessage.temp_name;
                        temp.cost=itemBuyOrErrorMessage.temp_cost;
                        temp.icon=itemBuyOrErrorMessage.temp_icon;
                        temp.quantity=1;
                        temp_list_selectedItems.Add(temp);

                        //updating the icn here

                        //updating the coin amount here
                        GameManager.instance.CoinCount-=cost;
                        CoinCount.text=""+GameManager.instance.CoinCount;
                        
                        //update the panel slots
                        UpdateThePanelSlots(itemBuyOrErrorMessage.temp_name,itemBuyOrErrorMessage.temp_icon);

                        cantAddMore.gameObject.SetActive(false);
                        transform.parent.gameObject.SetActive(false);
                        AudioManager.instance.BuyOptionSound();
                    }

                }
                //updating the list
                GameManager.instance.listOfSelectedItems=temp_list_selectedItems;
            } 
            else
            {
                AudioManager.instance.ErrorSound();
                //play the animation
                Handheld.Vibrate();
            }

        }
        else
        {
            cantAddMore.gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void UpdateThePanelSlots(ItemName name,Sprite icon)
    {
        for(int i=0;i<itemBuyOrErrorMessage.item_slots.Length;i++)
        {
            if(itemBuyOrErrorMessage.item_slots[i].name==name.ToString())
            {
                //updating the count of that respective slot
                var temp_text=itemBuyOrErrorMessage.item_slots[i].item_slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                if(temp_text.text.Length==0)
                {
                    temp_text.text="2";
                }
                else
                {
                    var count=(int)double.Parse(temp_text.text);
                    count++;
                    temp_text.text=""+count;
                }
                break;

            }
            else
            {
                if(!itemBuyOrErrorMessage.item_slots[i].used)
                {
                    //updating the whole slot
                    itemBuyOrErrorMessage.item_slots[i].name=name.ToString();
                    itemBuyOrErrorMessage.item_slots[i].used=true;
                    itemBuyOrErrorMessage.item_slots[i].item_slot.GetComponent<Image>().sprite=icon;
                    break;
                }
            }
        }
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
