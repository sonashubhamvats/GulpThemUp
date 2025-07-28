using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSceneManagerInMainMenu : MonoBehaviour
{
    public GameObject[] mainMenu;

    public GameObject[] levelSelector;

    public GameObject[] Settings,customize_select,player_select,cannon_select;
    
    public GameObject ItemSelectionPanel;
    public static InSceneManagerInMainMenu instance;

    public List<GameObject> activeScene=new List<GameObject>();
    private int flag=0;

    private void Awake() {
        MakeInstance();
        flag=0;
        BackToTheMainMenu();
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
    public void BackToTheMainMenu()
    {
        if(flag==1)
        {
            AudioManager.instance.ButtonClickSound();
        }
        else
        {
            flag=1;
        }
        for(int i=0;i<activeScene.Count;i++)
        {
            activeScene[i].SetActive(false);
        }
        for(int i=0;i<mainMenu.Length;i++)
        {
            mainMenu[i].SetActive(true);
        }
        for(int i=0;i<mainMenu.Length;i++)
        {
            activeScene.Add(mainMenu[i]);
        }
    }
    public void callLevelSelector()
    {
        AudioManager.instance.ButtonClickSound();
        for(int i=0;i<activeScene.Count;i++)
        {
            activeScene[i].SetActive(false);
        }
        for(int i=0;i<levelSelector.Length;i++)
        {
            levelSelector[i].SetActive(true);
        }
        for(int i=0;i<levelSelector.Length;i++)
        {
            activeScene.Add(levelSelector[i]);
        }

    }
    public void callSettings()
    {
        AudioManager.instance.ButtonClickSound();
        for(int i=0;i<activeScene.Count;i++)
        {
            activeScene[i].SetActive(false);
        }
        for(int i=0;i<Settings.Length;i++)
        {
            Settings[i].SetActive(true);
        }
        for(int i=0;i<Settings.Length;i++)
        {
            activeScene.Add(Settings[i]);
        }

    }

    public void toggleLevelItemSelectionPanel(bool info,int level)
    {
        AudioManager.instance.ButtonClickSound();
        if(info)
        {
            ItemSelectionPanel.gameObject.SetActive(true);
            ItemPanelScript.instance.UpdateTheStatsInThePanel(level);
        }
        else
        {
            ItemSelectionPanel.gameObject.SetActive(false);
        }
        
    }
    public void Customize_select()
    {
        AudioManager.instance.ButtonClickSound();
        for(int i=0;i<activeScene.Count;i++)
        {
            activeScene[i].SetActive(false);
        }
        for(int i=0;i<customize_select.Length;i++)
        {
            customize_select[i].SetActive(true);
        }
        for(int i=0;i<customize_select.Length;i++)
        {
            activeScene.Add(customize_select[i]);
        }

    }
    public void Customize_player()
    {
        AudioManager.instance.ButtonClickSound();
        for(int i=0;i<activeScene.Count;i++)
        {
            activeScene[i].SetActive(false);
        }
        for(int i=0;i<player_select.Length;i++)
        {
            player_select[i].SetActive(true);
        }
        for(int i=0;i<player_select.Length;i++)
        {
            activeScene.Add(player_select[i]);
        }

    }
    public void Customize_Cannon()
    {
        AudioManager.instance.ButtonClickSound();
        for(int i=0;i<activeScene.Count;i++)
        {
            activeScene[i].SetActive(false);
        }
        for(int i=0;i<cannon_select.Length;i++)
        {
            cannon_select[i].SetActive(true);
        }
        for(int i=0;i<cannon_select.Length;i++)
        {
            activeScene.Add(cannon_select[i]);
        }

    }
}
