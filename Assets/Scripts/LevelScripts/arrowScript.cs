using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class arrowScript : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public int arrowType;
    public TextMeshProUGUI[] levelIndicator;
    public int set=1;

    public GameObject otherArrow;

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if(arrowType==1)
        {
            if(set<46)
            {
                set+=5;
                otherArrow.GetComponent<arrowScript>().set+=5;
                for(int i=0;i<levelIndicator.Length;i++)
                {
                    levelIndicator[i].text="Level-"+(set+i);
                    levelIndicator[i].transform.parent.GetComponent<LevelTouchScript>().level=(set+i);
                }

            }
        }
        else
        {
            
            if(set>=6)
            {
                
                set-=5;
                otherArrow.GetComponent<arrowScript>().set-=5;
                for(int i=0;i<levelIndicator.Length;i++)
                {
                    levelIndicator[i].text="Level-"+(set+i);
                    levelIndicator[i].transform.parent.GetComponent<LevelTouchScript>().level=(set+i);
                }

            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    // Start is called before the first frame update
    private void OnEnable() {
        for(int i=0;i<levelIndicator.Length;i++)
        {
            levelIndicator[i].text="Level-"+(set+i);
        }
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
