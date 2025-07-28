using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ObjectiveManager : MonoBehaviour
{
    public int[] rewardExpPoints=new int[3];

    public Color gray;
    public Image[] upper_slot_star,lower_slot_star;

    public TextMeshProUGUI[] objectives;
    public int[] objectiveNos=new int[3];
    public string[] objectivePoints=new string[3]; 
    public static ObjectiveManager instance;
    private void Awake()
    {
        MakeInstance();
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
    public void UpdateTheObjectives(int levelNo)
    {
        var LevelTotal=51+(levelNo-1)*50;
       
        int increment=levelNo/5;
        if(levelNo%5==0&&increment!=0)
        {
        
            increment--;
        }
        var noOfScenes=3+increment;
        
        int singleSceneTotal=LevelTotal/noOfScenes;

        objectiveNos[0]=(int)(-0.5f*singleSceneTotal);
        objectiveNos[1]=(int)(0.1*singleSceneTotal);
        objectiveNos[2]=(int)(0.3*singleSceneTotal);
        
        for(int i=0;i<rewardExpPoints.Length;i++)
        {
            rewardExpPoints[i]=LevelTotal+100*i;
            if(i!=0)
            {
                objectivePoints[i]="Main score>"+objectiveNos[i]+" to get a bonus "+rewardExpPoints[i]+"xp";
            }
            else
            {
                objectivePoints[i]="Main score>"+objectiveNos[i]+" to stay in the level";
            }
            objectives[i].text=objectivePoints[i];
        }

        if(levelNo<=GameManager.instance.current_max_level)
        {
            //level played before
            int starNo=GameManager.instance.stars_in_level[levelNo-1];
            UpdateTheStarSlots(starNo);
        }
        else
        {
            //level not played before
            UpdateTheStarSlots(0);
        }

        //updating the Gamemanager's data for the objective manager
        for(int i=0;i<3;i++)
        {
            GameManager.instance.rewardXpPoints[i]=rewardExpPoints[i];
            GameManager.instance.objNos[i]=objectiveNos[i];
            GameManager.instance.objPoints[i]=objectivePoints[i];
        }

    }
    private void UpdateTheStarSlots(int starno)
    {
        //stars gained
        for(int i=0;i<starno;i++)
        {
            upper_slot_star[i].color=Color.white;
            lower_slot_star[i].color=Color.white;
        }
        //stars not there
        for(int i=starno;i<3;i++)
        {
            upper_slot_star[i].color=gray;
            lower_slot_star[i].color=gray;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
