using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]
public struct tutSep
{
    public string tutName;
    public string[] messages;

    public int[] intervals;

    public GameObject[] pointers;

    public int[] indexAtPointer;

    public bool[] allTrue;
}
public class TutorialManager : MonoBehaviour
{
    public Color[] probable_Colors;

    int curr_color_index=0;

    public Image tutorialScreenBorder;
    public TextMeshProUGUI sceneCounter;
    public TextMeshProUGUI tutText;
    public tutSep[] allTuts;
    public bool inSpawnMessage;
    public GameObject[] allTheObjects;
    public Image nextPointer,TutMessage;

    public static TutorialManager instance;
    
    //this will handle the pointers that come with each tut step
    public int associatePointer=0;
    public int sepMessageIndex,tutTypeIndex;
    public int end;
    string temp="";

    public TextMeshProUGUI tutorialTimer;

    public GameObject tutorialGameObject;

    float timer=0;
    private void Awake() 
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
    // Start is called before the first frame update
    
    void Start()
    {
       
        
        if(GameManager.instance.InTutorial)
        {
            end=0;       
            sepMessageIndex=0;
            tutTypeIndex=0;
            ChangeTheColorOfTheTutorial();
            EnableDisableObjects();
            TouchMovement.instance.clutch=true;
            TutMessage.gameObject.SetActive(true);
            Time.timeScale=0f;
            SpawnMessage(allTuts[tutTypeIndex].messages,ref sepMessageIndex,ref end);
        }
        
    }
    void ChangeTheColorOfTheTutorial()
    {
        tutorialScreenBorder.color=probable_Colors[curr_color_index];
        tutText.color=probable_Colors[curr_color_index];
        nextPointer.transform.GetChild(0).GetComponent<Image>().color=probable_Colors[curr_color_index];
        for(int i=0;i<allTuts[tutTypeIndex].pointers.Length;i++)
        {
            allTuts[tutTypeIndex].pointers[i].transform.GetChild(0).GetComponent<Image>().color=probable_Colors[curr_color_index];
        }
        if(curr_color_index==(probable_Colors.Length-1))
        {
            curr_color_index=0;
            return;
        }
        curr_color_index++;

    }
    private void Update() {
        if(GameManager.instance.InTutorial)
            decrementTimer();
    }
    public void CompleteTheWholeMessage()
    {
        if(!nextPointer.gameObject.activeInHierarchy)
        {
            end=allTuts[tutTypeIndex].messages[sepMessageIndex].Length;
            tutText.text=allTuts[tutTypeIndex].messages[sepMessageIndex];

        }
    }
    // Update is called once per frame
    void EnableDisableObjects()
    {
        for(int i=0;i<allTheObjects.Length;i++)
        {
            allTheObjects[i].SetActive(allTuts[tutTypeIndex].allTrue[i]);      
        }

    }
    void SpawnMessage(string[] messages,ref int index,ref int counter)
    {
        if(counter<messages[index].Length)
        {
            temp+=messages[index][counter];
            tutText.text=temp;
            counter++;        
            inSpawnMessage=true;

        }
        if(counter<messages[index].Length)
        {
            StartCoroutine(callMessageFunction());
        }
        else
        {
            inSpawnMessage=false;
            nextPointer.gameObject.SetActive(true);
        }
    }
    public void decrementTimer()
    {
        if(timer>0)
        {
            timer-=Time.deltaTime;
            tutorialTimer.text=timer.ToString("F0")+"s";
        }
        else
        {
            tutorialGameObject.SetActive(false);
        }
    }
    public void NextTutPrepare()
    {
        AudioManager.instance.ButtonClickSound();
        
        //bullet firing mech
        if(tutTypeIndex==1&&sepMessageIndex==0)
        {
            TouchMovement.instance.clutch=false;
        }
        //bullet firing mech ends

        //grenn pellet mech
        if(tutTypeIndex==1&&sepMessageIndex==3)
        {
            TouchMovement.instance.clutch=false;
            EnemySpawner.instance.SpawnNumberFromTutorialManager(true);
            
        }
        //green pellet mech ends

        //colored pellets
        if(tutTypeIndex==1&&sepMessageIndex==6)
        {
            TouchMovement.instance.clutch=false;
            
            EnemySpawner.instance.SpawnNumberFromTutorialManager(true,true);
        }
        //red pellets
        if(tutTypeIndex==1&&sepMessageIndex==8)
        {
            TouchMovement.instance.clutch=false;
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
        }
        //shield item
        if(tutTypeIndex==2&&sepMessageIndex==3)
        {
            TouchMovement.instance.clutch=false;
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.friend=false;

        }
        //push item
        if(tutTypeIndex==2&&sepMessageIndex==5)
        {
            TouchMovement.instance.clutch=false;
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.friend=false;
            
        }
        //portal item
        if(tutTypeIndex==2&&sepMessageIndex==7)
        {
            EnemySpawner.instance.friend=false;
            TouchMovement.instance.clutch=false;
            
        }

        //positive segment
        if(tutTypeIndex==3&&sepMessageIndex==1)
        {
            TouchMovement.instance.clutch=false;            
            EnemySpawner.instance.SpawnNumberFromTutorialManager(true,true);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(true,true);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(true);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(true);
            StartCoroutine(BringDownTheGameSceneChangerMidTutorial());
            EnemySpawner.instance.friend=true;
        }
        //negative segment
        if(tutTypeIndex==3&&sepMessageIndex==3)
        {
            TouchMovement.instance.clutch=false;            
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.SpawnNumberFromTutorialManager(false);
            EnemySpawner.instance.friend=false;
            StartCoroutine(BringDownTheGameSceneChangerMidTutorial());

        }
        //end message
        if(tutTypeIndex==allTuts.Length-1&&sepMessageIndex==allTuts[tutTypeIndex].messages.Length-1)
        {
            GameManager.instance.listOfSelectedItems.Clear();
            ButtonManager.instance.loadingScene.SetActive(true);
            StartCoroutine(ButtonManager.instance.LoadSceneAsync("MainMenu"));

        }
        //scene pointer
        if(tutTypeIndex==3&&sepMessageIndex==5)
        {
            SceneCounter.instance.stopBlinking=false;
        }
        //the end is present 
        if(tutTypeIndex==3&&sepMessageIndex==10)
        {
            Debug.Log("Tutorial done - ");
            GameManager.instance.InTutorial=false;
        }
        nextPointer.gameObject.SetActive(false);
        TutMessage.gameObject.SetActive(false);
        for(var i=0;i<allTuts[tutTypeIndex].pointers.Length;i++)
        {
            allTuts[tutTypeIndex].pointers[i].SetActive(false);
        }
        Time.timeScale=1f;

        //setting the timer variable
        timer=allTuts[tutTypeIndex].intervals[sepMessageIndex];
        if(timer>0)
        {
            tutorialGameObject.SetActive(true);
        }

        StartCoroutine(executeTheNextTutSeq());
    }
    IEnumerator BringDownTheGameSceneChangerMidTutorial()
    {
        yield return new WaitForSeconds(10f);
        EnemySpawner.instance.tempGameSceneChanger.SetActive(true);
        StartCoroutine(DestroyTheGameChanger());
    }
    IEnumerator DestroyTheGameChanger()
    {
        yield return new WaitForSeconds(2f);
        Destroy(EnemySpawner.instance.tempGameSceneChanger);
        EnemySpawner.instance.tempGameSceneChanger=Instantiate(EnemySpawner.instance.gameSceneChanger,EnemySpawner.instance.StartPointOfTheGameSceneChanger,Quaternion.identity);
        EnemySpawner.instance.tempGameSceneChanger.SetActive(false);

    }
    //next tut execution with the delay
    IEnumerator executeTheNextTutSeq()
    {
        yield return new WaitForSeconds(allTuts[tutTypeIndex].intervals[sepMessageIndex]);
        end=0;       
        sepMessageIndex++;
        
        //for now
        if(sepMessageIndex<allTuts[tutTypeIndex].messages.Length)
        {
            SpawnTutMessage();
        }
        else
        {
            if(tutTypeIndex<(allTuts.Length-1))
            {
                tutTypeIndex++;
                ChangeTheColorOfTheTutorial();
                associatePointer=0;
                sepMessageIndex=0;
                EnableDisableObjects();
                SpawnTutMessage();
            }
            else
            {
                //tutorial done when all the messages have been displayed
                GameManager.instance.tutDone=true;

            }
        }

    }

    //this is basically for turning on the pointers in the tutorial screen
    public void SpawnTutMessage()
    {
        //scene pointer
        if(tutTypeIndex==3&&sepMessageIndex==5)
        {
            var color_text=SceneCounter.instance.counter_text.color;
            var color_border=SceneCounter.instance.border_colors.color;
            color_text.a=1f;
            color_border.a=1f;
            SceneCounter.instance.counter_text.color=color_text;
            SceneCounter.instance.border_colors.color=color_border;
            sceneCounter.text="1/3";
            SceneCounter.instance.stopBlinking=true;
        }

        Debug.Log("Here");
        TutMessage.gameObject.SetActive(true);
        temp="";
        if(associatePointer<allTuts[tutTypeIndex].indexAtPointer.Length)
        {
            if(sepMessageIndex==allTuts[tutTypeIndex].indexAtPointer[associatePointer])
            {
                //this is an arrangement to switch on the next pointer in the game screen 
                
                allTuts[tutTypeIndex].pointers[associatePointer].SetActive(true);
                associatePointer++;
            }
 
        }
        Time.timeScale=0f;

        //clutching the player
        TouchMovement.instance.clutch=true;
        PlayerAttack.instance.fire=false;
        //ends

        SpawnMessage(allTuts[tutTypeIndex].messages,ref sepMessageIndex,ref end);
    }
    IEnumerator callMessageFunction()
    {
        yield return new WaitForSecondsRealtime(0.04f);
        SpawnMessage(allTuts[tutTypeIndex].messages,ref sepMessageIndex,ref end);
    }
}
