using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class levelAssinger : MonoBehaviour
{
    public GameObject cantPlayNow;
    public Color redColor,green,gray;
    private void OnEnable()
    {
        for(int i=0;i<50;i++)
        {
            if(GameManager.instance.rank*5>i)
            {
                transform.GetChild(i).GetComponent<Image>().color=Color.red;
                transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
                if(i<=GameManager.instance.current_max_level)
                {
                    if(i==GameManager.instance.current_max_level)
                    {
                        transform.GetChild(i).GetComponent<Image>().color=gray;    
                    }
                    else
                    {
                        transform.GetChild(i).GetComponent<Image>().color=green;

                    }

                }
            }
            else
            {
                transform.GetChild(i).GetComponent<Image>().color=Color.red;
                transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
            }
            transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text="Lv-"+(i+1);
            transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize=28;
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
