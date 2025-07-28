using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class buy_cannon_player : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    //also includes the assignment of the message in the are u sure message box
    private void OnEnable()
    {
        int childCount=transform.parent.parent.parent.childCount;
        for(int i=0;i<childCount-1;i++)
        {
            if(transform.parent.parent.parent.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.parent.parent.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text="This item will cost you"+" "+
                transform.parent.parent.parent.GetChild(i).gameObject.GetComponent<PlayerCharacterCannonIndiSelection>().price+"c , Proceed??";
            }
        }
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        int childCount=transform.parent.parent.parent.childCount;
        for(int i=0;i<childCount-1;i++)
        {
            if(transform.parent.parent.parent.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.parent.parent.parent.GetChild(i).gameObject.GetComponent<PlayerCharacterCannonIndiSelection>().Buy();
            }
        }
        //transform.parent.parent.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
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
