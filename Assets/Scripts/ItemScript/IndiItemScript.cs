using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class IndiItemScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public ItemInfo info;

    //one more unlocked bool kept here to keep the item unlocked at every time

    public GameObject item_final_panel;
    private bool locked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //setting the locked variable
    public void UpdateTheLockedStatus(int levelNo)
    {
        if((GameManager.instance.current_max_level+1)<info.unlocks_at)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            locked=true;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            locked=false;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(locked)
        {
            Handheld.Vibrate();
            item_final_panel.GetComponent<ItemBuyOrErrorMessage>().ShowErrorSegment(info._name.ToString(),info.cost,info.unlocks_at);
        }
        else
        {
            var temp_sprite=GetComponent<Image>().sprite;
            item_final_panel.GetComponent<ItemBuyOrErrorMessage>().ShowBuyOrNotSegment(info.cost,info._name,temp_sprite,info.maximumAllowed);
        }    
        
    }
}
