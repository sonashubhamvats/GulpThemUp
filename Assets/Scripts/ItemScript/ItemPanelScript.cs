using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemPanelScript : MonoBehaviour
{
    public TextMeshProUGUI heading;
    public static ItemPanelScript instance;

    public GameObject[] items_in_panel;
    [HideInInspector]public int curr_level;
    // Start is called before the first frame update
    private void OnEnable() {
        
    }
    private void Awake() {
        MakeInstance();
    }
    void Start()
    {
        
    }
    void MakeInstance()
    {
        if(instance==null)
        {
            instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateTheStatsInThePanel(int levelNo)
    {
        ObjectiveManager.instance.UpdateTheObjectives(levelNo);
        curr_level=levelNo;
        //assinging the head out script
        FindObjectOfType<HeadOutInPanel>().AssignTheLevelNo(curr_level);
        
        //assingign the heading
        heading.text="Level-"+levelNo;
        for(int i=0;i<items_in_panel.Length;i++)
        {
            items_in_panel[i].GetComponent<IndiItemScript>().UpdateTheLockedStatus(curr_level);
        }
    }
}
