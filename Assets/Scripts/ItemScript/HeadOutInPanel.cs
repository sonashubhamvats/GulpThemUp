using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HeadOutInPanel : MonoBehaviour,IPointerDownHandler
{
    private int levelNo;

    //called from the itempanel script 
    public void AssignTheLevelNo(int levelno)
    {
        levelNo=levelno;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.startGame(levelNo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
