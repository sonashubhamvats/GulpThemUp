using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public enum CannonPlayer
{
    Cannon,
    Player
}
public class PlayerCharacterCannonIndiSelection : MonoBehaviour
{
    public RectTransform _this_rect;

    public float scale_x_unlocked;
    public Vector3 offSetForCannonplacement;
    public int price;

    public int unlocks_at;

    //only for the player script
    public int coolDown,shieldTime;

    public int gun_type;
    public Button select,buy;
    public CannonPlayer type;

    public Sprite _sprite;

    public GameObject cant_buy,are_u_sure;
    public TextMeshProUGUI cover_message;

    public Sprite cover_image;
    
    public GameObject[] slots;
    public Image this_image;
    public Material _material;

    public Sprite[] slot_images;
    private int curr_index;
    public Sprite questionMark;
    private void OnEnable()
    {
        Debug.Log("onEnable");
        this_image.sprite=_sprite;
        curr_index=((int)double.Parse(gameObject.name))-1;
        cover_message.gameObject.SetActive(false);
        if(type==CannonPlayer.Cannon)
        {
            for(int i=0;i<4;i++)
            {
                slots[i].GetComponent<Image>().sprite=slot_images[i];
            }

        }
        else
        {
            for(int i=0;i<slots.Length;i++)
            {
                slots[i].GetComponent<Image>().sprite=null;
                slots[i].GetComponent<Image>().color=Color.black;
                slots[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        //here we check for the rank to check if the item has been unlocked or not
        //if unlocked then
        if(unlocks_at<=GameManager.instance.current_max_level)
        {
            if(type==CannonPlayer.Cannon)
            {
                var temp= _this_rect.localScale;
                temp.x=scale_x_unlocked;
                _this_rect.localScale=temp;
                for(int i=0;i<gun_type;i++)
                {
                    var temp_color=slots[i].GetComponent<Image>().color;
                    temp_color.a=1f;
                    slots[i].GetComponent<Image>().color=temp_color;
                }
                for(int i=gun_type;i<4;i++)
                {
                    var temp_color=slots[i].GetComponent<Image>().color;
                    temp_color.a=0f;
                    slots[i].GetComponent<Image>().color=temp_color;
                }
            }
            else
            {
                slots[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=shieldTime+"s";
                slots[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=coolDown+"s";
            }
            //check about the level in here to decide whether to put om the lock or not
            var temp_buy_color=buy.gameObject.GetComponent<Image>().color;
            var temp_select_color=gameObject.GetComponent<Image>().color;

            if(type==CannonPlayer.Player)
            {
                if(GameManager.instance.selected_player[curr_index])
                {
                    temp_buy_color.a=0f;
                    //disabling the textmesh pro here
                    buy.transform.GetChild(0).gameObject.SetActive(false);
                    buy.GetComponent<Button>().enabled=false;
                    temp_select_color.a=1f;
                    temp_select_color=Color.blue;
                    select.transform.GetChild(0).gameObject.SetActive(true);
                    select.GetComponent<Button>().enabled=true;
                    
                    
                }
                else
                {
                    if(GameManager.instance.bought_player[curr_index])
                    {
                        temp_buy_color.a=0f;
                        buy.transform.GetChild(0).gameObject.SetActive(false);
                        buy.GetComponent<Button>().enabled=false;
                        temp_select_color.a=1f;
                        temp_select_color=Color.gray;
                        select.transform.GetChild(0).gameObject.SetActive(true);
                        select.GetComponent<Button>().enabled=true;
                    }
                    else
                    {
                        temp_select_color.a=0f;
                        select.transform.GetChild(0).gameObject.SetActive(false);
                        select.GetComponent<Button>().enabled=false;
                        temp_buy_color.a=1f;
                        buy.transform.GetChild(0).gameObject.SetActive(true);
                        buy.GetComponent<Button>().enabled=true;

                    }

                }
            }
            else
            {
                if(GameManager.instance.selected_cannon[curr_index])
                {
                    temp_buy_color.a=0f;
                    buy.transform.GetChild(0).gameObject.SetActive(false);
                    temp_select_color.a=1f;
                    temp_select_color=Color.blue;
                    select.transform.GetChild(0).gameObject.SetActive(true);
                    buy.GetComponent<Button>().enabled=false;
                    select.GetComponent<Button>().enabled=true;
                
                
                }
                else
                {
                    if(GameManager.instance.bought_cannon[curr_index])
                    {
                        temp_buy_color.a=0f;
                        buy.transform.GetChild(0).gameObject.SetActive(false);
                        temp_select_color.a=1f;
                        temp_select_color=Color.gray;
                        select.transform.GetChild(0).gameObject.SetActive(true);
                        buy.GetComponent<Button>().enabled=false;
                        select.GetComponent<Button>().enabled=true;
                    }
                    else
                    {
                        temp_select_color.a=0f;
                        select.transform.GetChild(0).gameObject.SetActive(false);
                        temp_buy_color.a=1f;
                        buy.transform.GetChild(0).gameObject.SetActive(true);
                        buy.GetComponent<Button>().enabled=true;
                        select.GetComponent<Button>().enabled=false;
                    }

                }

            }
            buy.gameObject.GetComponent<Image>().color=temp_buy_color;
            select.gameObject.GetComponent<Image>().color=temp_select_color;

        }
        else
        {
            this_image.sprite=cover_image;
            cover_message.gameObject.SetActive(true);
            cover_message.text="Unlocks at level "+unlocks_at;
            if(type==CannonPlayer.Cannon)
            {
                var temp= _this_rect.localScale;
                temp.x=1f;
                _this_rect.localScale=temp;

                for(int i=0;i<slots.Length;i++)
                {
                    slots[i].GetComponent<Image>().sprite=questionMark;
                }
            }
            else
            {
                for(int i=0;i<slots.Length;i++)
                {
                    slots[i].GetComponent<Image>().sprite=questionMark;
                    slots[i].GetComponent<Image>().color=Color.white;
                    slots[i].transform.GetChild(0).gameObject.SetActive(false);
                }

            }
            var temp_buy_color=buy.GetComponent<Image>().color;
            var temp_select_color=select.GetComponent<Image>().color;
            temp_buy_color.a=0f;
            temp_select_color.a=0f;
            buy.transform.GetChild(0).gameObject.SetActive(false);
            select.transform.GetChild(0).gameObject.SetActive(false);
            buy.GetComponent<Button>().enabled=false;
            select.GetComponent<Button>().enabled=false;
            buy.GetComponent<Image>().color=temp_buy_color;
            select.GetComponent<Image>().color=temp_select_color;
        }
        

    }
    
    public void Buy()
    {
        var temp_buy_color=buy.gameObject.GetComponent<Image>().color;
        var temp_select_color=gameObject.GetComponent<Image>().color;

        if(are_u_sure.activeInHierarchy)
        {
            if(GameManager.instance.CoinCount>=price)
            {
                AudioManager.instance.BuyOptionSound();
                //updating the coin count
                GameManager.instance.CoinCount-=price;
                var CoinCount=GameObject.Find("Coin").GetComponent<TextMeshProUGUI>();
                CoinCount.text=""+GameManager.instance.CoinCount;
                //updating the bought 
                temp_buy_color.a=0f;
                buy.transform.GetChild(0).gameObject.SetActive(false);
                temp_select_color.a=1f;
                temp_select_color=Color.gray;
                select.transform.GetChild(0).gameObject.SetActive(true);

                buy.GetComponent<Button>().enabled=false;
                select.GetComponent<Button>().enabled=true;


                if(type==CannonPlayer.Cannon)
                {
                    GameManager.instance.bought_cannon[curr_index]=true;
                }
                else
                {
                    GameManager.instance.bought_player[curr_index]=true;
                }
                are_u_sure.gameObject.SetActive(false);
            }
            else
            {
                cant_buy.SetActive(false);
                cant_buy.SetActive(true);
            }

        }
        else
        {
            AudioManager.instance.SelectOptionSound();
            are_u_sure.SetActive(true);
            cant_buy.SetActive(false);
        }
        buy.gameObject.GetComponent<Image>().color=temp_buy_color;
        select.gameObject.GetComponent<Image>().color=temp_select_color;

    }    
    public void Cancel()
    {
        are_u_sure.gameObject.SetActive(false);
    }
    public void Select()
    {
        //childcount-1 everywhere to ignore the last element which is the are u sure notification 
        for(int i=0;i<transform.parent.childCount-1;i++)
        {
            if(i!=curr_index)
            {
                Debug.Log("here");
                transform.parent.GetChild(i).gameObject.GetComponent<PlayerCharacterCannonIndiSelection>().select.
                GetComponent<Image>().color=Color.gray;
                if(type==CannonPlayer.Cannon)
                {
                    GameManager.instance.selected_cannon[i]=false;
                }
                else
                {
                    GameManager.instance.selected_player[i]=false;
                }
                
            }
            else
            {
                AudioManager.instance.SelectOptionSound();
                select.GetComponent<Image>().color=Color.blue;
                if(type==CannonPlayer.Cannon)
                {
                    GameManager.instance.selected_cannon[i]=true;
                    //selected then writing in the gun type for deciding which types of bullets it would shoot
                    GameManager.instance.gun_type=gun_type;
                    
                }
                else
                {
                    GameManager.instance.selected_player[i]=true;
                    GameManager.instance.shieldTime=shieldTime;
                    GameManager.instance.cooldown=coolDown;

                }
            }
        }
    }
}
