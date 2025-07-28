using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class LevelTouchScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private TextMeshProUGUI text;
    [HideInInspector]public int level;
    private bool locked;

    
    private void Start() {
        text=transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        string[] split=new string[2];
        split=text.text.Split('-');
        
        level=(int)double.Parse(split[1]);
        if(transform.GetChild(2).gameObject.activeInHierarchy)
        {
            transform.parent.GetComponent<levelAssinger>().cantPlayNow.SetActive(true);
            int increment=level/5;
            if(level%5==0&&increment!=0)
            {
            
                increment--;
            }
            increment++;
            AudioManager.instance.ErrorSound();
            transform.parent.GetComponent<levelAssinger>().cantPlayNow.transform.GetChild(0)
            .GetComponent<TextMeshProUGUI>().text="Unlocks at Rank- "+increment;

        }
        else
        {
            if(transform.GetComponent<Image>().color==Color.red)
            {
                AudioManager.instance.ErrorSound();
                transform.parent.GetComponent<levelAssinger>().cantPlayNow.SetActive(true);
                transform.parent.GetComponent<levelAssinger>().cantPlayNow.transform.GetChild(0)
                .GetComponent<TextMeshProUGUI>().text="Complete Level "+(level-1)+" first";

            }
            else
            {
                InSceneManagerInMainMenu.instance.toggleLevelItemSelectionPanel(true,level);

            }
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Touch Up");
    }    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
