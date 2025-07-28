using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum arrowType
{
    left,
    right
}
public class arrow_in_selection : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public arrowType type;
    public int total;
    private void OnEnable()
    {
        //the main head of the collection
        total=transform.parent.parent.parent.parent.childCount-1;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.instance.SelectOptionSound();
        //the topmost parent which contains 1-2-3-... as thier names
        int index=((int)(double.Parse(transform.parent.parent.parent.gameObject.name)))-1;
        int new_index;
        if(type==arrowType.right)
        {
            if(index==(total-1))
            {
                new_index=0;
            }
            else
            {
                new_index=index+1;
            }
        }
        else
        {
            if(index==0)
            {
                new_index=total-1;
            }
            else
            {
                new_index=index-1;
            }
        }
        transform.parent.parent.parent.parent.GetChild(new_index).gameObject.SetActive(true);
        transform.parent.parent.parent.parent.GetChild(index).gameObject.SetActive(false);
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
